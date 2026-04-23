using Api.Core.Exceptions;

namespace Api.Features.Roles;

public class RoleBusinessRules(
  IRoleRepository _roleRepository,
  ILogger<RoleBusinessRules> _logger)
{
  public async Task<Role> GetRoleIfExistAsync(
    Guid id,
    Func<IQueryable<Role>, IQueryable<Role>>? include = null,
    bool enableTracking=false,
    CancellationToken cancellationToken = default)
  {
    var role = await _roleRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

    if (role == null)
    {
      _logger.LogWarning("Rol bulunamadı. Aranan ID: {RoleId}", id);

      throw new NotFoundException("Rol bulunamadı.");
    }

    return role;
  }

  public async Task RoleNameMustBeUniqueAsync(
    string name,
    Guid? id = null,
    CancellationToken cancellationToken = default)
  {
    var exists = await _roleRepository.AnyAsync(r => r.Name == name && (id == null || r.Id != id), cancellationToken);

    if (exists)
    {
      _logger.LogWarning("Rol adı zaten mevcut. Çakışan ad: {RoleName}", name);

      throw new BusinessException("Bu rol adı zaten mevcut.");
    }
  }

  public void RoleMustNotBeSystemRole(string roleName)
  {
    string[] systemRoles = ["Admin", "SuperAdmin"];

    if (systemRoles.Contains(roleName))
    {
      _logger.LogWarning("Sistem rolü üzerinde yetkisiz işlem denemesi! Rol: {RoleName}", roleName);

      throw new BusinessException("Sistem rolleri üzerinde bu işlem yapılamaz.");
    }
  }
}
