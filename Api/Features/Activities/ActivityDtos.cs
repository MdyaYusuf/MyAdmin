namespace Api.Features.Activities;

public sealed record CreateActivityRequest(
    string Action,           
    string EntityName,       
    string? EntityId = null, 
    string? OldValues = null, 
    string? NewValues = null, 
    string? IPAddress = null,
    bool IsSuccess = true,
    Guid? UserId = null     
);

public class ActivityResponseDto
{
  public Guid Id { get; set; }
  public string Action { get; set; } = null!;
  public string EntityName { get; set; } = null!;
  public string? EntityId { get; set; }
  public string? OldValues { get; set; }
  public string? NewValues { get; set; }
  public string? IPAddress { get; set; }
  public bool IsSuccess { get; set; }
  public Guid? UserId { get; set; }
  public string? UserName { get; set; } 
  public DateTime CreatedDate { get; set; }
}

public sealed record ActivityPreviewDto(
    Guid Id,
    string Action,
    string EntityName,
    bool IsSuccess,
    DateTime CreatedDate);