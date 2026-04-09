namespace Api.Features.Authentication;

public static class AuthenticationRegistration
{
    public static IServiceCollection AddAuthenticationFeatures(this IServiceCollection services)
    {
      services.AddScoped<AuthenticationBusinessRules>();
      services.AddScoped<IAuthenticationService, AuthenticationService>();

      return services;
  }
}
