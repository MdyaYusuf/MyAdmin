using Api.Core.Controllers;
using Api.Features.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Authentication;

[Route("api/[controller]")]
[ApiController]
public class AuthsController(IAuthenticationService _authService) : CustomBaseController
{
  [HttpPost("register")]
  public async Task<IActionResult> Register(
    [FromBody] RegisterUserRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _authService.RegisterAsync(request, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(
    [FromBody] LoginRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _authService.LoginAsync(request, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPost("refresh-token")]
  public async Task<IActionResult> RefreshToken(
    [FromBody] string refreshToken,
    CancellationToken cancellationToken)
  {
    var result = await _authService.RefreshTokenAsync(refreshToken, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPost("revoke-token")]
  [Authorize]
  public async Task<IActionResult> RevokeToken(
    [FromBody] string refreshToken,
    CancellationToken cancellationToken)
  {
    var result = await _authService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);

    return CreateActionResult(result);
  }
}