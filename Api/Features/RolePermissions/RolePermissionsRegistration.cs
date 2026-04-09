namespace Api.Features.RolePermissions;

public static class RolePermissionsRegistration
{
  public static IServiceCollection AddRolePermissionDependencies(this IServiceCollection services)
  {
    services.AddScoped<IRolePermissionRepository, EfRolePermissionRepository>();
    services.AddSingleton<RolePermissionMapper>();
    services.AddScoped<RolePermissionBusinessRules>();
    services.AddScoped<IRolePermissionService, RolePermissionService>();

    return services;
  }
}
