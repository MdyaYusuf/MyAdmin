using Api.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Api.Features.Permissions;

public class PermissionBusinessRules(
  IPermissionRepository _permissionRepository,
  ILogger<PermissionBusinessRules> _logger)
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
      _logger.LogWarning("Yetki kaydı bulunamadı. Aranan ID: {PermissionId}", id);

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
      _logger.LogWarning("Yetki ismi çakışması! Bu isim zaten mevcut: {PermissionName}", name);

      throw new BusinessException("Bu izin adı zaten mevcut.");
    }
  }
}