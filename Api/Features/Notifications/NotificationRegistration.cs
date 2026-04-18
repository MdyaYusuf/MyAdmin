namespace Api.Features.Notifications;

public static class NotificationRegistration
{
  public static IServiceCollection AddNotificationDependencies(this IServiceCollection services)
  {
    services.AddScoped<INotificationRepository, EfNotificationRepository>();
    services.AddScoped<INotificationService, NotificationService>();
    services.AddScoped<NotificationBusinessRules>();
    services.AddSingleton<NotificationMapper>();

    return services;
  }
}
