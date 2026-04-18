using Api.Core.Responses;
using System.Linq.Expressions;

namespace Api.Features.Notifications;

public interface INotificationService
{
  Task<ReturnModel<List<NotificationResponseDto>>> GetAllAsync(
    Expression<Func<Notification, bool>>? filter = null,
    Func<IQueryable<Notification>, IQueryable<Notification>>? include = null,
    Func<IQueryable<Notification>, IOrderedQueryable<Notification>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NotificationResponseDto>> GetAsync(
    Expression<Func<Notification, bool>> predicate,
    Guid userId, 
    Func<IQueryable<Notification>, IQueryable<Notification>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NotificationResponseDto>> GetByIdAsync(
    Guid id,
    Guid userId,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<CreatedNotificationResponseDto>> AddAsync(
    CreateNotificationRequest request,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> UpdateAsync(
    Guid id,
    UpdateNotificationRequest request,
    Guid userId,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> RemoveAsync(
    Guid id,
    Guid userId,
    CancellationToken cancellationToken = default);

  // Yardımcı Metodlar
  Task<ReturnModel<NoData>> MarkAsReadAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
  Task<ReturnModel<NoData>> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken = default);
  Task<ReturnModel<int>> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default);
}