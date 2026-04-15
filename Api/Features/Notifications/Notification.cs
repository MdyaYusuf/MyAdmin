using Api.Core.Entities;
using Api.Features.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Api.Features.Notifications;

public class Notification : Entity<Guid>
{
  [SetsRequiredMembers]
  public Notification()
  {
    Title = default!;
    Message = default!;
  }

  public required string Title { get; set; }
  public required string Message { get; set; }
  public string Type { get; set; } = "INFO";
  public string? LinkUrl { get; set; }
  public bool IsRead { get; set; } = false;

  // Semantic ReadAt instead of base entity UpdatedDate
  [NotMapped]
  public DateTime? ReadAt
  {
    get => UpdatedDate;
    set => UpdatedDate = value;
  }

  // Navigation properties
  public required Guid UserId { get; set; }
  public virtual User User { get; set; } = default!;
}