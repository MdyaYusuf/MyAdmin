using Api.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.RolePermissions;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class RolePermissionsController(IRolePermissionService _rolePermissionService) : CustomBaseController
{
  [HttpGet("role/{roleId:guid}")]
  public async Task<IActionResult> GetPermissionsByRoleId(Guid roleId, CancellationToken cancellationToken)
  {
    var result = await _rolePermissionService.GetPermissionsByRoleIdAsync(roleId, cancellationToken);
    return CreateActionResult(result);
  }

  [HttpPost]
  public async Task<IActionResult> Assign([FromBody] CreateRolePermissionRequest request, CancellationToken cancellationToken)
  {
    var result = await _rolePermissionService.AssignPermissionToRoleAsync(
        request.RoleId,
        request.PermissionId,
        cancellationToken);

    return CreateActionResult(result);
  }

  [HttpDelete("role/{roleId:guid}/permission/{permissionId:guid}")]
  public async Task<IActionResult> Revoke(Guid roleId, Guid permissionId, CancellationToken cancellationToken)
  {
    var result = await _rolePermissionService.RevokePermissionFromRoleAsync(
        roleId,
        permissionId,
        cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPost("sync/{roleId:guid}")]
  public async Task<IActionResult> Sync([FromRoute] Guid roleId, [FromBody] List<Guid> permissionIds, CancellationToken cancellationToken)
  {
    var result = await _rolePermissionService.SyncRolePermissionsAsync(
        roleId,
        permissionIds,
        cancellationToken);

    return CreateActionResult(result);
  }
}