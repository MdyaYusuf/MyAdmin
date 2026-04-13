using Api.Core.Repositories;
using Api.Core.Responses;
using Api.Core.Security;
using Api.Features.Roles;
using Api.Features.UserRoles;
using Api.Features.Users;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Features.Authentication;

public class AuthenticationService(
  IUserRepository _userRepository,
  IRoleRepository _roleRepository,
  UserBusinessRules _userBusinessRules,
  AuthenticationBusinessRules _authBusinessRules,
  UserMapper _mapper,
  IUnitOfWork _unitOfWork,
  IValidator<RegisterUserRequest> _registerValidator,
  IValidator<LoginRequest> _loginValidator,
  IOptions<TokenOptions> _tokenOptions) : IAuthenticationService
{
  private readonly TokenOptions _options = _tokenOptions.Value;

  public async Task<ReturnModel<TokenResponseDto>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
  {
    var validationResult = await _loginValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    User? user = await _userRepository.GetAsync(
      predicate: u => u.Email == request.Email,
      include: query => query.Include(u => u.UserRoles).ThenInclude(ur => ur.Role),
      cancellationToken: cancellationToken);

    _authBusinessRules.UserCredentialsMustMatch(user, request.Password);

    user!.RefreshToken = GenerateRefreshToken();
    user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiration);

    _userRepository.Update(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    TokenResponseDto tokenResponse = CreateToken(user!, user.RefreshToken);

    return new ReturnModel<TokenResponseDto>()
    {
      Data = tokenResponse,
      Success = true,
      StatusCode = 200,
      Message = "Giriş başarılı."
    };
  }

  public async Task<ReturnModel<CreatedUserResponseDto>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
  {
    var validationResult = await _registerValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    await _userBusinessRules.EmailMustBeUniqueAsync(request.Email, cancellationToken: cancellationToken);
    await _userBusinessRules.UsernameMustBeUniqueAsync(request.Username, cancellationToken: cancellationToken);

    User createdUser = _mapper.RegisterToEntity(request);

    var defaultRole = await _roleRepository.GetAsync(
      r => r.Name == "User",
      cancellationToken: cancellationToken);

    if (defaultRole != null)
    {
      createdUser.UserRoles.Add(new UserRole
      {
        UserId = createdUser.Id,
        RoleId = defaultRole.Id
      });
    }

    HashingHelper.CreatePasswordHash(request.Password, out string hash, out string key);
    createdUser.PasswordHash = hash;
    createdUser.PasswordKey = key;

    await _userRepository.AddAsync(createdUser, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    CreatedUserResponseDto response = _mapper.EntityToCreatedResponseDto(createdUser);

    return new ReturnModel<CreatedUserResponseDto>()
    {
      Success = true,
      Message = "Kaydınız başarıyla tamamlandı.",
      Data = response,
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<TokenResponseDto>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
  {
    User? user = await _userRepository.GetAsync(
      predicate: u => u.RefreshToken == refreshToken,
      include: query => query.Include(u => u.UserRoles).ThenInclude(ur => ur.Role),
      cancellationToken: cancellationToken);

    _authBusinessRules.RefreshTokenMustBeValid(user, refreshToken);

    string currentRefreshToken = user!.RefreshToken!;

    if (user!.RefreshTokenExpiration <= DateTime.UtcNow.AddDays(1))
    {
      currentRefreshToken = GenerateRefreshToken();
      user.RefreshToken = currentRefreshToken;
      user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(_options.RefreshTokenExpiration);
    }

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    TokenResponseDto tokenResponse = CreateToken(user!, currentRefreshToken);

    return new ReturnModel<TokenResponseDto>()
    {
      Data = tokenResponse,
      Success = true,
      StatusCode = 200,
      Message = "Oturum tazelendi."
    };
  }

  public async Task<ReturnModel<NoData>> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
  {
    User? user = await _userRepository.GetAsync(
      u => u.RefreshToken == refreshToken,
      cancellationToken: cancellationToken);

    _authBusinessRules.RefreshTokenUserMustExist(user);

    user!.RefreshToken = null;
    user.RefreshTokenExpiration = null;

    _userRepository.Update(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>()
    {
      Success = true,
      StatusCode = 200,
      Message = "Oturum sonlandırıldı."
    };
  }

  private TokenResponseDto CreateToken(User user, string refreshToken)
  {
    var claims = new List<Claim>()
    {
      new(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new(ClaimTypes.Email, user.Email),
      new(ClaimTypes.Name, user.Username)
    };

    if (user.UserRoles != null)
    {
      foreach (var userRole in user.UserRoles.Where(ur => ur.Role != null))
      {
        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
      }
    }

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    var expiration = DateTime.UtcNow.AddMinutes(_options.AccessTokenExpiration);

    var token = new JwtSecurityToken(
      issuer: _options.Issuer,
      audience: _options.Audience,
      claims: claims,
      expires: expiration,
      signingCredentials: creds);

    return new TokenResponseDto(
      new JwtSecurityTokenHandler().WriteToken(token),
      expiration,
      refreshToken,
      _mapper.EntityToResponseDto(user));
  }

  private string GenerateRefreshToken()
  {
    return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
  }
}
