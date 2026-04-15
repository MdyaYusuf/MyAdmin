namespace Api.Features.Activities;

public static class ActivityRegistration
{
  public static IServiceCollection AddActivityDependencies(this IServiceCollection services)
  {
    services.AddScoped<IActivityRepository, EfActivityRepository>();

    return services;
  }
}
