using Api.Core.Entities;
using Api.Features.Roles;
using Api.Features.Users;
using System.Diagnostics.CodeAnalysis;

namespace Api.Features.UserRoles;

public class UserRole : Entity<Guid>
{
  [SetsRequiredMembers]
  public UserRole()
  {

  }

  public required Guid UserId { get; set; }
  public virtual User User { get; set; } = default!;

  public required Guid RoleId { get; set; }
  public virtual Role Role { get; set; } = default!;
}
