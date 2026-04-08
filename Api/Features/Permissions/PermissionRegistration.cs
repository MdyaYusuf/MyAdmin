namespace Api.Features.Permissions;

public static class PermissionRegistration
{
  public static IServiceCollection AddPermissionDependencies(this IServiceCollection services)
  {
    services.AddScoped<IPermissionRepository, EfPermissionRepository>();

    return services;
  }
}
