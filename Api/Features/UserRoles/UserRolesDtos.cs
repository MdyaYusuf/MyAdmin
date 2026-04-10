namespace Api.Features.UserRoles;

public sealed record CreateUserRoleRequest(
  Guid UserId,
  Guid RoleId);

public class UpdateUserRoleRequest
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public Guid RoleId { get; set; }
}

public class UserRoleResponseDto
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string Username { get; set; } = null!;
  public Guid RoleId { get; set; }
  public string RoleName { get; set; } = null!;
  public DateTime CreatedDate { get; set; }
}

public sealed record CreatedUserRoleResponseDto(
  Guid Id,
  Guid UserId,
  Guid RoleId);

public sealed record UserRolePreviewDto(
  Guid Id,
  string Username,
  string RoleName);
