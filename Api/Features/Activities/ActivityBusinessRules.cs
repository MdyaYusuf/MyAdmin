using Api.Core.Exceptions;

namespace Api.Features.Activities;

public class ActivityBusinessRules(IActivityRepository _activityRepository)
{
  public async Task<Activity> GetActivityIfExistAsync(Guid id)
  {
    var activity = await _activityRepository.GetByIdAsync(id, enableTracking: false);

    if (activity == null)
    {
      throw new NotFoundException("Aktivite kaydı bulunamadı.");
    }

    return activity;
  }

  public void AnonymousActivityOnlyVisibleToAdmin(Activity activity, string currentUserRole)
  {
    if (activity.UserId == null && currentUserRole != "Admin")
    {
      throw new ForbiddenException("Sistem aktivitelerini görüntüleme yetkiniz yok.");
    }
  }
}