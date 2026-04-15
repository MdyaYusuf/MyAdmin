using Api.Core.Entities;
using Api.Features.Users;
using System.Diagnostics.CodeAnalysis;

namespace Api.Features.Activities;

public class Activity : Entity<Guid>
{
  [SetsRequiredMembers]
  public Activity()
  {
    Action = default!;
    EntityName = default!;
  }

  public Guid? UserId { get; set; }
  public virtual User? User { get; set; }
  public required string Action { get; set; }
  public required string EntityName { get; set; }
  public string? EntityId { get; set; }
  public string? OldValues { get; set; }
  public string? NewValues { get; set; }
  public string? IPAddress { get; set; }
  public bool IsSuccess { get; set; } = true;
}
