using Api.Core.Exceptions;

namespace Api.Features.RolePermissions;

public class RolePermissionBusinessRules(IRolePermissionRepository _rolePermissionRepository)
{
  public async Task<RolePermission> GetRolePermissionIfExistAsync(Guid id, CancellationToken cancellationToken = default)
  {
    var rp = await _rolePermissionRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

    if (rp == null)
    {
      throw new NotFoundException("Rol-İzin ilişkisi bulunamadı.");
    } 

    return rp;
  }

  public async Task RolePermissionRelationMustNotBeDuplicateAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken = default)
  {
    var exists = await _rolePermissionRepository.AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId, cancellationToken);

    if (exists)
    {
      throw new BusinessException("Bu rol zaten bu izne sahip.");
    }
  }
}
