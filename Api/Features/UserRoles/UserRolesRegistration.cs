namespace Api.Features.UserRoles;

public static class UserRolesRegistration
{
  public static IServiceCollection AddUserRoleDependencies(this IServiceCollection services)
  {
    services.AddScoped<IUserRoleRepository, EfUserRoleRepository>();
    services.AddSingleton<UserRoleMapper>();
    services.AddScoped<UserRoleBusinessRules>();
    services.AddScoped<IUserRoleService, UserRoleService>();

    return services;
  }
}
