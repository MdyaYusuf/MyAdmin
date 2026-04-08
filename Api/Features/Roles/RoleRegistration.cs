namespace Api.Features.Roles;

public static class RoleRegistration
{
  public static IServiceCollection AddRoleDependencies(this IServiceCollection services)
  {
    services.AddScoped<IRoleRepository, EfRoleRepository>();
    services.AddSingleton<RoleMapper>();

    return services;
  }
}
