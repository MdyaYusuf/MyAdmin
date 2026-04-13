using Api.Core.Exceptions;

namespace Api.Features.Permissions;

public class PermissionBusinessRules(IPermissionRepository _permissionRepository)
{
  public async Task<Permission> GetPermissionIfExistAsync(
    Guid id,
    Func<IQueryable<Permission>, IQueryable<Permission>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default)
  {
    var permission = await _permissionRepository.GetByIdAsync(
      id,
      include,
      enableTracking,
      cancellationToken);

    if (permission == null)
    {
      throw new NotFoundException("İzin bulunamadı.");
    }

    return permission;
  }

  public async Task PermissionNameMustBeUniqueAsync(
    string name,
    Guid? id = null,
    CancellationToken cancellationToken = default)
  {
    var exists = await _permissionRepository.AnyAsync(
      p => p.Name == name && (id == null || p.Id != id),
      cancellationToken);

    if (exists)
    {
      throw new BusinessException("Bu izin adı zaten mevcut.");
    }
  }
}