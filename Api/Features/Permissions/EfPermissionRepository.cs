using Api.Core.Repositories;
using Api.Data;

namespace Api.Features.Permissions;

public class EfPermissionRepository : EfBaseRepository<BaseDbContext, Permission, Guid>, IPermissionRepository
{
  public EfPermissionRepository(BaseDbContext context) : base(context)
  {

  }
}
