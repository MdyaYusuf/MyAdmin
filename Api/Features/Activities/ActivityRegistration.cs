namespace Api.Features.Activities;

public static class ActivityRegistration
{
  public static IServiceCollection AddActivityDependencies(this IServiceCollection services)
  {
    services.AddScoped<IActivityRepository, EfActivityRepository>();
    services.AddScoped<IActivityService, ActivityService>();
    services.AddScoped<ActivityBusinessRules>();
    services.AddSingleton<ActivityMapper>();

    return services;
  }
}
