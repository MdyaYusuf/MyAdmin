using Api.Core.Repositories;

namespace Api.Features.RolePermissions;

public interface IRolePermissionRepository : IRepository<RolePermission, Guid>
{

}
