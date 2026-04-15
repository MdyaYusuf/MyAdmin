using Api.Core.Helpers;
using Api.Core.Repositories;
using Api.Core.Responses;
using Api.Core.Security;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Features.Users;

public class UserService(
  IUserRepository _userRepository,
  UserMapper _mapper,
  UserBusinessRules _businessRules,
  IUnitOfWork _unitOfWork,
  IValidator<UpdateUserRequest> _updateValidator,
  IValidator<ChangePasswordRequest> _changePasswordValidator) : IUserService
{
  public async Task<ReturnModel<List<UserResponseDto>>> GetAllAsync(
    Expression<Func<User, bool>>? filter = null, 
    Func<IQueryable<User>, IQueryable<User>>? include = null, 
    Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, 
    bool enableTracking = false, 
    bool withDeleted = false, 
    CancellationToken cancellationToken = default)
  {
    List<User> users = await _userRepository.GetAllAsync(
      include: u => u.Include(u => u.UserRoles).ThenInclude(ur => ur.Role),
      cancellationToken: cancellationToken);

    List<UserResponseDto> response = _mapper.EntityToResponseDtoList(users);

    return new ReturnModel<List<UserResponseDto>>()
    {
      Success = true,
      Message = "Kullanıcı listesi başarılı bir şekilde getirildi",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<UserResponseDto>> GetAsync(
    Expression<Func<User, bool>> predicate, 
    Func<IQueryable<User>, IQueryable<User>>? include = null, 
    bool enableTracking = false, 
    CancellationToken cancellationToken = default)
  {
    var user = await _userRepository.GetAsync(
      predicate: predicate,
      include: u => u.Include(u => u.UserRoles).ThenInclude(ur => ur.Role),
      enableTracking: enableTracking, 
      cancellationToken: cancellationToken);

    if (user == null)
    {
      return new ReturnModel<UserResponseDto>()
      {
        Success = false,
        Message = "Kullanıcı bulunamadı",
        Data = null,
        StatusCode = 200
      };
    }

    return new ReturnModel<UserResponseDto>()
    {
      Success = true,
      Message = "Kullanıcı başarılı bir şekilde getirildi",
      Data = _mapper.EntityToResponseDto(user),
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<UserResponseDto>> GetByIdAsync(
    Guid id, 
    Func<IQueryable<User>, IQueryable<User>>? include = null, 
    bool enableTracking = false, 
    CancellationToken cancellationToken = default)
  {
    User user = await _businessRules.GetUserIfExistAsync(
      id: id,
      include: u => u.Include(u => u.UserRoles).ThenInclude(ur => ur.Role),
      enableTracking: enableTracking,
      cancellationToken: cancellationToken);

    UserResponseDto response = _mapper.EntityToResponseDto(user);

    return new ReturnModel<UserResponseDto>()
    {
      Success = true,
      Message = "Kullanıcı başarılı bir şekilde getirildi",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> RemoveAsync(
    Guid id,
    Guid currentUserId,
    string userRole,
    CancellationToken cancellationToken = default)
  {
    _businessRules.UserMustBeOwnerOrAdmin(id, currentUserId, userRole);

    User user = await _businessRules.GetUserIfExistAsync(
      id,
      enableTracking: true,
      cancellationToken: cancellationToken);

    string? imagePathToDelete = user.ProfileImageUrl;

    _userRepository.Delete(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    FileHelper.DeleteImageFromDisk(imagePathToDelete);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Kullanıcı başarılı bir şekilde silindi",
      Data = null,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> UpdateAsync(
    UpdateUserRequest request,
    Guid currentUserId,
    CancellationToken cancellationToken = default)
  {
    var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    User existingUser = await _businessRules.GetUserIfExistAsync(
      currentUserId,
      enableTracking: true,
      cancellationToken: cancellationToken);

    if (existingUser.Email != request.Email)
    {
      await _businessRules.EmailMustBeUniqueAsync(request.Email, existingUser.Id, cancellationToken);
    }

    if (existingUser.Username != request.Username)
    {
      
      await _businessRules.UsernameMustBeUniqueAsync(request.Username, existingUser.Id, cancellationToken);
    }

    _mapper.UpdateEntityFromRequest(request, existingUser);

    existingUser.ProfileImageUrl = await FileHelper.ReplaceImageOnDisk(
      request.ImageFile,
      existingUser.ProfileImageUrl,
      "profiles",
      request.Username,
      cancellationToken);

    _userRepository.Update(existingUser);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Kullanıcı başarılı bir şekilde güncellendi",
      Data = null,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> ChangePasswordAsync(
    ChangePasswordRequest request,
    Guid userId,
    CancellationToken cancellationToken = default)
  {
    var validationResult = await _changePasswordValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    User user = await _businessRules.GetUserIfExistAsync(userId, enableTracking: true, cancellationToken: cancellationToken);

    _businessRules.PasswordMustMatch(request.CurrentPassword, user.PasswordHash, user.PasswordKey);

    HashingHelper.CreatePasswordHash(request.NewPassword, out string newHash, out string newKey);
    user.PasswordHash = newHash;
    user.PasswordKey = newKey;

    _userRepository.Update(user);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Şifreniz başarıyla güncellendi.",
      StatusCode = 200
    };
  }
}
