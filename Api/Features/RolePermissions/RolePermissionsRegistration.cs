namespace Api.Features.RolePermissions;

public static class RolePermissionsRegistration
{
  public static IServiceCollection AddRolePermissionDependencies(this IServiceCollection services)
  {
    services.AddScoped<IRolePermissionRepository, EfRolePermissionRepository>();

    return services;
  }
}
