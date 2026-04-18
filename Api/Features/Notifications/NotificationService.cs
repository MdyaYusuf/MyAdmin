using Api.Core.Repositories;
using Api.Core.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Features.Notifications;

public class NotificationService(
  INotificationRepository _notificationRepository,
  NotificationMapper _mapper,
  NotificationBusinessRules _businessRules,
  IUnitOfWork _unitOfWork,
  IValidator<CreateNotificationRequest> _createValidator,
  IValidator<UpdateNotificationRequest> _updateValidator) : INotificationService
{
  public async Task<ReturnModel<CreatedNotificationResponseDto>> AddAsync(
    CreateNotificationRequest request,
    CancellationToken cancellationToken = default)
  {
    var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    Notification createdNotification = _mapper.CreateToEntity(request);

    await _notificationRepository.AddAsync(createdNotification, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

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
    var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    Notification existingNotification = await _businessRules.GetNotificationAndCheckOwnershipAsync(id, userId, enableTracking: true);

    _mapper.UpdateEntityFromRequest(request, existingNotification);

    _notificationRepository.Update(existingNotification);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

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
    Notification notification = await _businessRules.GetNotificationAndCheckOwnershipAsync(id, userId, enableTracking: true);

    _notificationRepository.Delete(notification);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

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
    Notification notification = await _businessRules.GetNotificationAndCheckOwnershipAsync(id, userId, enableTracking: true);

    _businessRules.NotificationMustNotBeAlreadyRead(notification);

    notification.IsRead = true;
    notification.ReadAt = DateTime.UtcNow;

    await _unitOfWork.SaveChangesAsync(cancellationToken);

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