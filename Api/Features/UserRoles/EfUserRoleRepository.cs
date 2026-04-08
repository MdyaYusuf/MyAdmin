using Api.Core.Repositories;
using Api.Data;

namespace Api.Features.UserRoles;

public class EfUserRoleRepository : EfBaseRepository<BaseDbContext, UserRole, Guid>, IUserRoleRepository
{
  public EfUserRoleRepository(BaseDbContext context) : base(context)
  {

  }
}
