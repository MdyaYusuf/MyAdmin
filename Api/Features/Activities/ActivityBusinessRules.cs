using Api.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Api.Features.Activities;

public class ActivityBusinessRules(
  IActivityRepository _activityRepository,
  ILogger<ActivityBusinessRules> _logger)
{
  public async Task<Activity> GetActivityIfExistAsync(Guid id)
  {
    var activity = await _activityRepository.GetByIdAsync(id, enableTracking: false);

    if (activity == null)
    {
      _logger.LogWarning("Aktivite kaydı bulunamadı. Aranan ID: {ActivityId}", id);

      throw new NotFoundException("Aktivite kaydı bulunamadı.");
    }

    return activity;
  }

  public void AnonymousActivityOnlyVisibleToAdmin(Activity activity, string currentUserRole)
  {
    if (activity.UserId == null && currentUserRole != "Admin")
    {
      _logger.LogWarning("Yetkisiz sistem aktivitesi görüntüleme denemesi. Aktivite ID: {ActivityId}, Kullanıcı Rolü: {UserRole}",
          activity.Id, currentUserRole);

      throw new ForbiddenException("Sistem aktivitelerini görüntüleme yetkiniz yok.");
    }
  }
}