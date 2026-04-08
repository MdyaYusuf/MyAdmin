using Api.Core.Entities;
using Api.Features.RolePermissions;
using System.Diagnostics.CodeAnalysis;

namespace Api.Features.Permissions;

public class Permission : Entity<Guid>
{
  [SetsRequiredMembers]
  public Permission()
  {
    RolePermissions = new HashSet<RolePermission>();

    Name = default!;
  }

  public required string Name { get; set; }
  public string? Description { get; set; }

  // Navigation
  public virtual ICollection<RolePermission> RolePermissions { get; set; }
}
