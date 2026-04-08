using Api.Core.Repositories;
using Api.Data;

namespace Api.Features.RolePermissions;

public class EfRolePermissionRepository : EfBaseRepository<BaseDbContext, RolePermission, Guid>, IRolePermissionRepository
{
  public EfRolePermissionRepository(BaseDbContext context) : base(context)
  {

  }
}
