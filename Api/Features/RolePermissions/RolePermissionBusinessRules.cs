using Api.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Api.Features.RolePermissions;

public class RolePermissionBusinessRules(
  IRolePermissionRepository _rolePermissionRepository,
  ILogger<RolePermissionBusinessRules> _logger)
{
  public async Task<RolePermission> GetRolePermissionIfExistAsync(
    Guid id,
    CancellationToken cancellationToken = default)
  {
    var rolePermission = await _rolePermissionRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

    if (rolePermission == null)
    {
      _logger.LogWarning("Rol-İzin ilişkisi bulunamadı. Aranan ID: {RolePermissionId}", id);

      throw new NotFoundException("Rol-İzin ilişkisi bulunamadı.");
    } 

    return rolePermission;
  }

  public async Task RolePermissionRelationMustNotBeDuplicateAsync(
    Guid roleId,
    Guid permissionId,
    CancellationToken cancellationToken = default)
  {
    var exists = await _rolePermissionRepository.AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId, cancellationToken);

    if (exists)
    {
      _logger.LogWarning("Mevcut ilişki hatası: Bu rol zaten bu izne sahip. Rol ID: {RoleId}, Yetki ID: {PermissionId}",
          roleId, permissionId);

      throw new BusinessException("Bu rol zaten bu izne sahip.");
    }
  }
}
