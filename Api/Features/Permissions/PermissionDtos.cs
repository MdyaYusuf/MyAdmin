namespace Api.Features.Permissions;

public sealed record CreatePermissionRequest(
  string Name,
  string? Description);

public class UpdatePermissionRequest
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
}

public class PermissionResponseDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public DateTime CreatedDate { get; set; }
  public DateTime? UpdatedDate { get; set; }
}

public sealed record CreatedPermissionResponseDto(
  Guid Id,
  string Name);

public sealed record PermissionPreviewDto(
  Guid Id,
  string Name,
  string? Description);
