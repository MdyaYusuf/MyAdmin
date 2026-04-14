using Api.Core.Exceptions;
using Api.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Core.Controllers;

public class CustomBaseController : ControllerBase
{
  [NonAction]
  public IActionResult CreateActionResult<T>(ReturnModel<T> result)
  {
    return new ObjectResult(result)
    {
      StatusCode = result.StatusCode
    };
  }

  [NonAction]
  protected Guid GetUserId()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid guid))
    {
      throw new AuthorizationException("İşlem için giriş yapmanız gerekmektedir.");
    }

    return guid;
  }

  [NonAction]
  protected Guid? TryGetUserId()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    return Guid.TryParse(userId, out var id) ? id : null;
  }

  [NonAction]
  protected List<string> GetUserRoles()
  {
    return User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
  }

  [NonAction]
  protected bool IsAdmin()
  {
    return GetUserRoles().Contains("Admin");
  }
}
