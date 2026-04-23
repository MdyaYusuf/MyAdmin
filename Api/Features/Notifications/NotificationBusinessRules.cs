using Api.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Api.Features.Notifications;

public class NotificationBusinessRules(
  INotificationRepository _notificationRepository,
  ILogger<NotificationBusinessRules> _logger)
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
      _logger.LogWarning("Bildirim bulunamadı. Aranan ID: {NotificationId}", id);

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
      _logger.LogWarning("Yetkisiz bildirim erişim denemesi! Bildirim ID: {NotificationId}, Sahibi: {OwnerId}, Deneyen: {RequesterId}",
          notification.Id, notification.UserId, userId);

      throw new ForbiddenException("Bu bildirime erişim yetkiniz yok.");
    }
  }

  public void NotificationMustNotBeAlreadyRead(Notification notification)
  {
    if (notification.IsRead)
    {
      _logger.LogWarning("Bildirim zaten okunmuş olarak işaretlenmiş. Bildirim ID: {NotificationId}", notification.Id);

      throw new BusinessException("Bildirim zaten okundu olarak işaretlenmiş.");
    }
  }

  public async Task AtLeastOneUnreadNotificationMustExistAsync(Guid userId)
  {
    var hasUnread = await _notificationRepository.AnyAsync(n => n.UserId == userId && !n.IsRead);

    if (!hasUnread)
    {
      _logger.LogWarning("Okunmamış bildirim bulunmamaktadır. Kullanıcı: {UserId}", userId);

      throw new BusinessException("Okunmamış bildiriminiz bulunmamaktadır.");
    }
  }
}