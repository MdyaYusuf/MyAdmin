using Api.Core.Responses;
using Api.Features.Users;

namespace Api.Features.Authentication;

public class AuthenticationService : IAuthenticationService
{
  public Task<ReturnModel<TokenResponseDto>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<TokenResponseDto>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<CreatedUserResponseDto>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<NoData>> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
