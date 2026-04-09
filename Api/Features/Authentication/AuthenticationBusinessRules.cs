using Api.Core.Exceptions;
using Api.Core.Security;
using Api.Features.Users;

namespace Api.Features.Authentication;

public class AuthenticationBusinessRules
{
  public void UserCredentialsMustMatch(User? user, string password)
  {
    if (user == null || !HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordKey))
    {
      throw new BusinessException("Eposta veya şifre hatalı.");
    }
  }

  public void RefreshTokenMustBeValid(User? user, string providedRefreshToken)
  {
    
    if (user == null)
    {
      throw new NotFoundException("Oturum bulunamadı.");
    }

    if (user.RefreshToken != providedRefreshToken)
    {
      throw new AuthorizationException("Geçersiz oturum anahtarı.");
    }

    if (user.RefreshTokenExpiration < DateTime.UtcNow)
    {
      throw new AuthorizationException("Oturum süresi dolmuş, lütfen tekrar giriş yapın.");
    }
  }

  public void RefreshTokenUserMustExist(User? user)
  {
    if (user == null)
    {
      throw new NotFoundException("Token bulunamadı.");
    }
  }
  public void UserMustHavePermission(List<string> userPermissions, string requiredPermission)
  {
    if (!userPermissions.Contains(requiredPermission))
    {
      throw new ForbiddenException($"Bu işlem için '{requiredPermission}' iznine sahip olmanız gerekir.");
    }
  }
}