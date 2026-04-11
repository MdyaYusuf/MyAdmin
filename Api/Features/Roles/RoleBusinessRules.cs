using Api.Core.Exceptions;

namespace Api.Features.Roles;

public class RoleBusinessRules(IRoleRepository _roleRepository)
{
  public async Task<Role> GetRoleIfExistAsync(Guid id, Func<IQueryable<Role>, IQueryable<Role>>? include = null, bool enableTracking=false, CancellationToken cancellationToken = default)
  {
    var role = await _roleRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

    if (role == null)
    {
      throw new NotFoundException("Rol bulunamadı.");
    }

    return role;
  }

  public async Task RoleNameMustBeUniqueAsync(string name, Guid? id = null, CancellationToken cancellationToken = default)
  {
    var exists = await _roleRepository.AnyAsync(r => r.Name == name && (id == null || r.Id != id), cancellationToken);

    if (exists)
    {
      throw new BusinessException("Bu rol adı zaten mevcut.");
    }
  }

  public void RoleMustNotBeSystemRole(string roleName)
  {
    string[] systemRoles = ["Admin", "SuperAdmin"];
    if (systemRoles.Contains(roleName))
    {
      throw new BusinessException("Sistem rolleri üzerinde bu işlem yapılamaz.");
    }
  }
}
