using Riok.Mapperly.Abstractions;

namespace Api.Features.Permissions;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class PermissionMapper
{
  public partial Permission CreateToEntity(CreatePermissionRequest request);

  public partial void UpdateEntityFromRequest(UpdatePermissionRequest request, Permission entity);

  public partial PermissionResponseDto EntityToResponseDto(Permission entity);

  public partial CreatedPermissionResponseDto EntityToCreatedResponseDto(Permission entity);

  public partial List<PermissionResponseDto> EntityToResponseDtoList(List<Permission> entities);

  public partial PermissionPreviewDto EntityToPreviewDto(Permission entity);

  public partial List<PermissionPreviewDto> EntityToPreviewDtoList(List<Permission> entities);
}