using Api.Core.Entities;
using Api.Features.Permissions;
using Api.Features.Roles;
using System.Diagnostics.CodeAnalysis;

namespace Api.Features.RolePermissions;

public class RolePermission : Entity<Guid>
{
  [SetsRequiredMembers]
  public RolePermission()
  {

  }

  public required Guid RoleId { get; set; }
  public virtual Role Role { get; set; } = default!;

  public required Guid PermissionId { get; set; }
  public virtual Permission Permission { get; set; } = default!;
}
