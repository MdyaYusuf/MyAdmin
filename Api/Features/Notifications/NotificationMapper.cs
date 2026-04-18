using Riok.Mapperly.Abstractions;

namespace Api.Features.Notifications;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class NotificationMapper
{
  public partial Notification CreateToEntity(CreateNotificationRequest request);

  public partial void UpdateEntityFromRequest(UpdateNotificationRequest request, Notification entity);

  public partial NotificationResponseDto EntityToResponseDto(Notification entity);

  public partial CreatedNotificationResponseDto EntityToCreatedResponseDto(Notification entity);

  public partial List<NotificationResponseDto> EntityToResponseDtoList(List<Notification> entities);

  public partial NotificationPreviewDto EntityToPreviewDto(Notification entity);

  public partial List<NotificationPreviewDto> EntityToPreviewDtoList(List<Notification> entities);
}