using Api.Core.Responses;
using Api.Features.Permissions;

namespace Api.Features.RolePermissions;

public interface IRolePermissionService
{
  Task<ReturnModel<NoData>> AssignPermissionToRoleAsync(
    Guid roleId,
    Guid permissionId,
    CancellationToken cancellationToken = default);
  Task<ReturnModel<NoData>> RevokePermissionFromRoleAsync(
    Guid roleId,
    Guid permissionId,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> SyncRolePermissionsAsync(
    Guid roleId,
    List<Guid> permissionIds,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<List<PermissionResponseDto>>> GetPermissionsByRoleIdAsync(
    Guid roleId,
    CancellationToken cancellationToken = default);
}