using Api.Core.Exceptions;
using Api.Features.Notifications;

namespace Api.Features.Notifications;

public class NotificationBusinessRules(INotificationRepository _notificationRepository)
{
  public async Task<Notification> GetNotificationAndCheckOwnershipAsync(
    Guid id,
    Guid userId,
    bool enableTracking = false)
  {
    var notification = await GetNotificationIfExistAsync(id, enableTracking);
    NotificationMustBelongToUser(notification, userId);

    return notification;
  }

  public async Task<Notification> GetNotificationIfExistAsync(
    Guid id,
    bool enableTracking = false)
  {
    var notification = await _notificationRepository.GetByIdAsync(id, enableTracking: enableTracking);

    if (notification == null)
    {
      throw new NotFoundException("Bildirim bulunamadı.");
    }

    return notification;
  }

  public void NotificationMustBelongToUser(
    Notification notification,
    Guid userId)
  {
    if (notification.UserId != userId)
    {
      throw new ForbiddenException("Bu bildirime erişim yetkiniz yok.");
    }
  }

  public void NotificationMustNotBeAlreadyRead(Notification notification)
  {
    if (notification.IsRead)
    {
      throw new BusinessException("Bildirim zaten okundu olarak işaretlenmiş.");
    }
  }

  public async Task AtLeastOneUnreadNotificationMustExistAsync(Guid userId)
  {
    var hasUnread = await _notificationRepository.AnyAsync(n => n.UserId == userId && !n.IsRead);

    if (!hasUnread)
    {
      throw new BusinessException("Okunmamış bildiriminiz bulunmamaktadır.");
    }
  }
}