using Api.Features.Permissions;

namespace Api.Features.Roles;

public sealed record CreateRoleRequest(
  string Name,
  string? Description,
  string? Label);

public sealed record UpdateRoleRequest(
  string Name,
  string? Description,
  string? Label);

public class RoleResponseDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public string? Label { get; set; }
  public DateTime CreatedDate { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public List<PermissionResponseDto> Permissions { get; set; } = new();
}

public sealed record CreatedRoleResponseDto(
  Guid Id,
  string Name);

public sealed record RolePreviewDto(
  Guid Id,
  string Name,
  string? Label);
