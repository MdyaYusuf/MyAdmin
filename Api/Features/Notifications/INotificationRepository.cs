using Api.Core.Repositories;

namespace Api.Features.Notifications;

public interface INotificationRepository : IRepository<Notification, Guid>
{

}
