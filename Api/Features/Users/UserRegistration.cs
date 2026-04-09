namespace Api.Features.Users;

public static class UserRegistration
{
  public static IServiceCollection AddUserDependencies(this IServiceCollection services)
  {
    services.AddScoped<IUserRepository, EfUserRepository>();
    services.AddSingleton<UserMapper>();
    services.AddScoped<UserBusinessRules>();
    services.AddScoped<IUserService, UserService>();

    return services;
  }
}
