using Api.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.UserRoles;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] 
public class UserRolesController(IUserRoleService _userRoleService) : CustomBaseController
{
  [HttpGet("user/{userId:guid}")]
  public async Task<IActionResult> GetRolesByUserId(
    Guid userId,
    CancellationToken cancellationToken)
  {
    var result = await _userRoleService.GetRolesByUserIdAsync(userId, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPost]
  public async Task<IActionResult> Assign(
    [FromBody] CreateUserRoleRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _userRoleService.AssignRoleAsync(
      request.UserId,
      request.RoleId,
      cancellationToken);

    return CreateActionResult(result);
  }

  [HttpDelete("user/{userId:guid}/role/{roleId:guid}")]
  public async Task<IActionResult> Revoke(
    Guid userId,
    Guid roleId,
    CancellationToken cancellationToken)
  {
    var result = await _userRoleService.RevokeRoleAsync(
      userId,
      roleId,
      cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPost("sync/{userId:guid}")]
  public async Task<IActionResult> Sync(
    [FromRoute] Guid userId,
    [FromBody] List<Guid> roleIds,
    CancellationToken cancellationToken)
  {
    var result = await _userRoleService.SyncUserRolesAsync(
      userId,
      roleIds,
      cancellationToken);

    return CreateActionResult(result);
  }
}