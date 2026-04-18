using Api.Core.Repositories;
using Api.Core.Responses;
using Api.Features.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.RolePermissions;

public class RolePermissionService(
  IRolePermissionRepository _rolePermissionRepository,
  IPermissionRepository _permissionRepository,
  RolePermissionBusinessRules _businessRules,
  PermissionMapper _mapper,
  IUnitOfWork _unitOfWork) : IRolePermissionService
{
  public async Task<ReturnModel<NoData>> AssignPermissionToRoleAsync(
    Guid roleId,
    Guid permissionId,
    CancellationToken cancellationToken = default)
  {
    await _businessRules.RolePermissionRelationMustNotBeDuplicateAsync(roleId, permissionId, cancellationToken);

    await _rolePermissionRepository.AddAsync(new RolePermission
    {
      RoleId = roleId,
      PermissionId = permissionId
    }, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>
    {
      Success = true,
      Message = "İzin başarıyla role atandı.",
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<List<PermissionResponseDto>>> GetPermissionsByRoleIdAsync(
    Guid roleId,
    CancellationToken cancellationToken = default)
  {
    var permissions = await _permissionRepository
      .Query(enableTracking: false)
      .Where(p => p.RolePermissions.Any(rp => rp.RoleId == roleId))
      .ToListAsync(cancellationToken);

    List<PermissionResponseDto> response = _mapper.EntityToResponseDtoList(permissions);

    return new ReturnModel<List<PermissionResponseDto>>
    {
      Success = true,
      Message = "Role ait izinler başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> RevokePermissionFromRoleAsync(
    Guid roleId,
    Guid permissionId,
    CancellationToken cancellationToken = default)
  {
    var rolePermission = await _rolePermissionRepository.GetAsync(
      predicate: rp => rp.RoleId == roleId && rp.PermissionId == permissionId,
      enableTracking: true,
      cancellationToken: cancellationToken);

    if (rolePermission == null) {

      return new ReturnModel<NoData>
      {
        Success = false,
        Message = "Belirtilen role ve izin ilişkisi bulunamadı.",
        StatusCode = 404
      };
    }

    _rolePermissionRepository.Delete(rolePermission);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>
    {
      Success = true,
      Message = "İzin başarıyla rolden kaldırıldı.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> SyncRolePermissionsAsync(
    Guid roleId,
    List<Guid> permissionIds,
    CancellationToken cancellationToken = default)
  {
    var currentRolePermissions = await _rolePermissionRepository.GetAllAsync(
      filter: rp => rp.RoleId == roleId,
      enableTracking: true, 
      cancellationToken: cancellationToken);

    var currentPermissionIds = currentRolePermissions.Select(rp => rp.PermissionId).ToHashSet();
    var targetPermissionIds = permissionIds.ToHashSet();

    var toDelete = currentRolePermissions
      .Where(rp => !targetPermissionIds.Contains(rp.PermissionId))
      .ToList();

    foreach (var rp in toDelete)
    {
      _rolePermissionRepository.Delete(rp);
    }

    var idsToAdd = permissionIds
      .Where(pId => !currentPermissionIds.Contains(pId))
      .ToList();

    foreach (var pId in idsToAdd)
    {
      await _rolePermissionRepository.AddAsync(new RolePermission
      {
        RoleId = roleId,
        PermissionId = pId
      }, cancellationToken);
    }

    if (toDelete.Any() || idsToAdd.Any())
    {
      await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    return new ReturnModel<NoData>
    {
      Success = true,
      Message = "Role izinleri başarıyla senkronize edildi.",
      StatusCode = 200
    };
  }
}
