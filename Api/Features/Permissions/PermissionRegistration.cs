namespace Api.Features.Permissions;

public static class PermissionRegistration
{
  public static IServiceCollection AddPermissionDependencies(this IServiceCollection services)
  {
    services.AddScoped<IPermissionRepository, EfPermissionRepository>();
    services.AddSingleton<PermissionMapper>();
    services.AddScoped<PermissionBusinessRules>();
    services.AddScoped<IPermissionService, PermissionService>();

    return services;
  }
}
