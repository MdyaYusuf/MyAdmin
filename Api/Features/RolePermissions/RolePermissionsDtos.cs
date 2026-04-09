namespace Api.Features.RolePermissions;

public sealed record CreateRolePermissionRequest(
  Guid RoleId,
  Guid PermissionId);

public class UpdateRolePermissionRequest
{
  public Guid Id { get; set; }
  public Guid RoleId { get; set; }
  public Guid PermissionId { get; set; }
}
public class RolePermissionResponseDto
{
  public Guid Id { get; set; }
  public Guid RoleId { get; set; }
  public string RoleName { get; set; } = null!;
  public Guid PermissionId { get; set; }
  public string PermissionName { get; set; } = null!;
  public DateTime CreatedDate { get; set; }
}

public sealed record CreatedRolePermissionResponseDto(
  Guid Id,
  Guid RoleId,
  Guid PermissionId);

public sealed record RolePermissionPreviewDto(
  Guid Id,
  string RoleName,
  string PermissionName);
