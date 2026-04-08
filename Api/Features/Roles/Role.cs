using Api.Core.Entities;
using Api.Features.RolePermissions;
using Api.Features.UserRoles;
using System.Diagnostics.CodeAnalysis;

namespace Api.Features.Roles;

public class Role : Entity<Guid>
{
  [SetsRequiredMembers]
  public Role()
  {
    UserRoles = new HashSet<UserRole>();
    RolePermissions = new HashSet<RolePermission>();

    Name = default!;
  }

  public required string Name { get; set; }
  public string? Description { get; set; }
  public string? Label { get; set; }

  // Navigation properties
  public virtual ICollection<UserRole> UserRoles { get; set; }
  public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
