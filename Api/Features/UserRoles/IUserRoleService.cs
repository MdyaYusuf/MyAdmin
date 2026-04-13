using Api.Core.Responses;
using Api.Features.Roles;

namespace Api.Features.UserRoles;

public interface IUserRoleService
{
  Task<ReturnModel<NoData>> AssignRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
  Task<ReturnModel<NoData>> RevokeRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
  Task<ReturnModel<NoData>> SyncUserRolesAsync(Guid userId, List<Guid> roleIds, CancellationToken cancellationToken);
  Task<ReturnModel<List<RoleResponseDto>>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
