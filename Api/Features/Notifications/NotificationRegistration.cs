namespace Api.Features.Notifications;

public static class NotificationRegistration
{
  public static IServiceCollection AddNotificationDependencies(this IServiceCollection services)
  {
    services.AddScoped<INotificationRepository, EfNotificationRepository>();

    return services;
  }
}
