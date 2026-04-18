namespace Api.Features.Notifications;

public sealed record CreateNotificationRequest(
    string Title,
    string Message,
    Guid UserId,
    string Type = "INFO",
    string? LinkUrl = null);

public class UpdateNotificationRequest
{
  public Guid Id { get; set; }
  public bool IsRead { get; set; }
}

public class NotificationResponseDto
{
  public Guid Id { get; set; }
  public string Title { get; set; } = null!;
  public string Message { get; set; } = null!;
  public string Type { get; set; } = null!;
  public string? LinkUrl { get; set; }
  public bool IsRead { get; set; }
  public DateTime? ReadAt { get; set; } 
  public DateTime CreatedDate { get; set; }
  public Guid UserId { get; set; }
}

public sealed record CreatedNotificationResponseDto(
    Guid Id,
    string Title,
    Guid UserId);

public sealed record NotificationPreviewDto(
    Guid Id,
    string Title,
    string Type,
    bool IsRead,
    DateTime CreatedDate);
