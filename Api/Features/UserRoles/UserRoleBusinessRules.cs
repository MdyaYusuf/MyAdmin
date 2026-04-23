using Api.Core.Exceptions;

namespace Api.Features.UserRoles;

public class UserRoleBusinessRules(
  IUserRoleRepository _userRoleRepository,
  ILogger<UserRoleBusinessRules> _logger)
{
  public async Task<UserRole> GetUserRoleIfExistAsync(
    Guid id,
    CancellationToken cancellationToken = default)
  {
    var userRole = await _userRoleRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

    if (userRole == null)
    {
      _logger.LogWarning("Kullanıcı-Rol ilişkisi bulunamadı. Aranan ID: {UserRoleId}", id);

      throw new NotFoundException("Kullanıcı, rol ilişkisi bulunamadı.");
    }

    return userRole;
  }

  public async Task UserRoleRelationMustNotBeDuplicateAsync(
    Guid userId,
    Guid roleId,
    CancellationToken cancellationToken = default)
  {
    var exists = await _userRoleRepository.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

    if (exists)
    {
      _logger.LogWarning("Mevcut rol atama hatası: Kullanıcı zaten bu role sahip. Kullanıcı ID: {UserId}, Rol ID: {RoleId}",
          userId, roleId);

      throw new BusinessException("Kullanıcı zaten bu role sahip.");
    }
  }
}
