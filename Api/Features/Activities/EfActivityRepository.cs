using Api.Core.Repositories;
using Api.Data;

namespace Api.Features.Activities;

public class EfActivityRepository : EfBaseRepository<BaseDbContext, Activity, Guid>, IActivityRepository
{
  public EfActivityRepository(BaseDbContext context) : base(context)
  {

  }
}
