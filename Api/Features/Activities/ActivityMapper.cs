using Riok.Mapperly.Abstractions;

namespace Api.Features.Activities;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class ActivityMapper
{
  public partial Activity CreateToEntity(CreateActivityRequest request);

  [MapProperty("User.Username", nameof(ActivityResponseDto.UserName))]
  public partial ActivityResponseDto EntityToResponseDto(Activity entity);

  public partial List<ActivityResponseDto> EntityToResponseDtoList(List<Activity> entities);

  public partial ActivityPreviewDto EntityToPreviewDto(Activity entity);

  public partial List<ActivityPreviewDto> EntityToPreviewDtoList(List<Activity> entities);
}