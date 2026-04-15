using Api.Core.Repositories;
using Api.Data;

namespace Api.Features.Notifications;

public class EfNotificationRepository : EfBaseRepository<BaseDbContext, Notification, Guid>, INotificationRepository
{
  public EfNotificationRepository(BaseDbContext context) : base(context)
  {

  }
}
