using Api.Core.Repositories;
using Api.Core.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Api.Features.Notifications;

public class NotificationService(
  INotificationRepository _notificationRepository,
  NotificationMapper _mapper,
  NotificationBusinessRules _businessRules,
  IUnitOfWork _unitOfWork,
  IValidator<CreateNotificationRequest> _createValidator,
  IValidator<UpdateNotificationRequest> _updateValidator,
  ILogger<NotificationService> _logger) : INotificationService
{
  public async Task<ReturnModel<CreatedNotificationResponseDto>> AddAsync(
    CreateNotificationRequest request,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Yeni bildirim oluşturma işlemi başlatıldı. Hedef Kullanıcı: {TargetUserId}", request.UserId);

    var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      _logger.LogWarning("Bildirim oluşturma validasyonu başarısız oldu. Kullanıcı: {TargetUserId}", request.UserId);

      throw new ValidationException(validationResult.Errors);
    }

    Notification createdNotification = _mapper.CreateToEntity(request);

    await _notificationRepository.AddAsync(createdNotification, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Bildirim başarıyla oluşturuldu. ID: {NotificationId}, Tip: {Type}", createdNotification.Id, createdNotification.Type);

    CreatedNotificationResponseDto response = _mapper.EntityToCreatedResponseDto(createdNotification);

    return new ReturnModel<CreatedNotificationResponseDto>()
    {
      Success = true,
      Message = "Bildirim başarıyla oluşturuldu.",
      Data = response,
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<List<NotificationResponseDto>>> GetAllAsync(
    Expression<Func<Notification, bool>>? filter = null,
    Func<IQueryable<Notification>, IQueryable<Notification>>? include = null,
    Func<IQueryable<Notification>, IOrderedQueryable<Notification>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Bildirimler listeleniyor.");

    List<Notification> notifications = await _notificationRepository.GetAllAsync(
      filter: filter,
      orderBy: q => q.OrderByDescending(n => n.CreatedDate),
      enableTracking: enableTracking,
      cancellationToken: cancellationToken);

    List<NotificationResponseDto> response = _mapper.EntityToResponseDtoList(notifications);

    return new ReturnModel<List<NotificationResponseDto>>()
    {
      Success = true,
      Message = "Bildirimler başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NotificationResponseDto>> GetAsync(
    Expression<Func<Notification, bool>> predicate,
    Guid userId,
    Func<IQueryable<Notification>, IQueryable<Notification>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Belirli kriterlere göre bildirim sorgulanıyor. Kullanıcı: {UserId}", userId);

    var notification = await _notificationRepository.GetAsync(
      predicate: predicate,
      enableTracking: enableTracking,
      cancellationToken: cancellationToken);

    if (notification != null)
    {
      _businessRules.NotificationMustBelongToUser(notification, userId);
    }

    if (notification == null)
    {
      _logger.LogWarning("Kriterlere uygun bildirim bulunamadı. Kullanıcı: {UserId}", userId);

      return new ReturnModel<NotificationResponseDto>()
      {
        Success = false,
        Message = "Bildirim bulunamadı.",
        StatusCode = 200
      };
    }

    NotificationResponseDto response = _mapper.EntityToResponseDto(notification);

    return new ReturnModel<NotificationResponseDto>()
    {
      Success = true,
      Message = "Bildirim başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NotificationResponseDto>> GetByIdAsync(
    Guid id,
    Guid userId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Bildirim detayları getiriliyor. ID: {NotificationId}, Kullanıcı: {UserId}", id, userId);

    Notification notification = await _businessRules.GetNotificationAndCheckOwnershipAsync(id, userId);

    NotificationResponseDto response = _mapper.EntityToResponseDto(notification);

    return new ReturnModel<NotificationResponseDto>()
    {
      Success = true,
      Message = "Bildirim detayları getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> UpdateAsync(
    Guid id,
    UpdateNotificationRequest request,
    Guid userId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Bildirim güncelleme işlemi başlatıldı. ID: {NotificationId}", id);

    var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      _logger.LogWarning("Bildirim güncelleme validasyonu başarısız oldu. ID: {NotificationId}", id);

      throw new ValidationException(validationResult.Errors);
    }

    Notification existingNotification = await _businessRules.GetNotificationAndCheckOwnershipAsync(id, userId, enableTracking: true);

    _mapper.UpdateEntityFromRequest(request, existingNotification);

    _notificationRepository.Update(existingNotification);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Bildirim başarıyla güncellendi. ID: {NotificationId}", id);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Bildirim durumu güncellendi.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> RemoveAsync(
    Guid id,
    Guid userId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Bildirim silme işlemi başlatıldı. ID: {NotificationId}, Kullanıcı: {UserId}", id, userId);

    Notification notification = await _businessRules.GetNotificationAndCheckOwnershipAsync(id, userId, enableTracking: true);

    _notificationRepository.Delete(notification);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Bildirim başarıyla silindi. ID: {NotificationId}", id);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Bildirim silindi.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> MarkAsReadAsync(
    Guid id,
    Guid userId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Bildirim okundu olarak işaretleniyor. ID: {NotificationId}, Kullanıcı: {UserId}", id, userId);

    Notification notification = await _businessRules.GetNotificationAndCheckOwnershipAsync(id, userId, enableTracking: true);

    _businessRules.NotificationMustNotBeAlreadyRead(notification);

    notification.IsRead = true;
    notification.ReadAt = DateTime.UtcNow;

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Bildirim başarıyla okundu olarak işaretlendi. ID: {NotificationId}", id);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Bildirim okundu olarak işaretlendi.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> MarkAllAsReadAsync(
    Guid userId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Kullanıcının tüm bildirimleri okundu olarak işaretleniyor. Kullanıcı: {UserId}", userId);

    await _businessRules.AtLeastOneUnreadNotificationMustExistAsync(userId);

    var unreadNotifications = await _notificationRepository.GetAllAsync(
      filter: n => n.UserId == userId && !n.IsRead,
      enableTracking: true,
      cancellationToken: cancellationToken);

    foreach (var notification in unreadNotifications)
    {
      notification.IsRead = true;
      notification.ReadAt = DateTime.UtcNow;
    }

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Kullanıcının ({UserId}) tüm bildirimleri başarıyla okundu olarak işaretlendi.", userId);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Tüm bildirimler okundu olarak işaretlendi.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<int>> GetUnreadCountAsync(
    Guid userId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Okunmamış bildirim sayısı sorgulanıyor. Kullanıcı: {UserId}", userId);

    var count = await _notificationRepository
      .Query(enableTracking: false)
      .CountAsync(n => n.UserId == userId && !n.IsRead, cancellationToken);

    return new ReturnModel<int>()
    {
      Success = true,
      Message = "Okunmamış bildirim sayısı getirildi.",
      Data = count,
      StatusCode = 200
    };
  }
}