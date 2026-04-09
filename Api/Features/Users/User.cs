using Api.Core.Entities;
using Api.Features.Roles;
using Api.Features.UserRoles;
using System.Diagnostics.CodeAnalysis;

namespace Api.Features.Users;

public class User : Entity<Guid>
{
  [SetsRequiredMembers]
  public User()
  {
    UserRoles = new HashSet<UserRole>();
    Roles = new HashSet<Role>();

    Username = default!;
    Email = default!;
    PasswordHash = default!;
    PasswordKey = default!;
  }

  public required string Username { get; set; }
  public required string Email { get; set; }
  public required string PasswordHash { get; set; }
  public required string PasswordKey { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime? RefreshTokenExpiration { get; set; }
  public string? ProfileImageUrl { get; set; }
  public string? Bio { get; set; }
  public bool IsActive { get; set; } = true;

  // Navigation properties
  public virtual ICollection<Role> Roles { get; set; }
  public virtual ICollection<UserRole> UserRoles { get; set; }
}
