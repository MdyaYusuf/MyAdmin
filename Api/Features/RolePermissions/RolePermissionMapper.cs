using Riok.Mapperly.Abstractions;

namespace Api.Features.RolePermissions;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class RolePermissionMapper
{
  public partial RolePermission CreateToEntity(CreateRolePermissionRequest request);

  public partial void UpdateEntityFromRequest(UpdateRolePermissionRequest request, RolePermission entity);

  [MapProperty("Role.Name", nameof(RolePermissionResponseDto.RoleName))]
  [MapProperty("Permission.Name", nameof(RolePermissionResponseDto.PermissionName))]
  public partial RolePermissionResponseDto EntityToResponseDto(RolePermission entity);

  public partial CreatedRolePermissionResponseDto EntityToCreatedResponseDto(RolePermission entity);

  public partial List<RolePermissionResponseDto> EntityToResponseDtoList(List<RolePermission> entities);

  [MapProperty("Role.Name", "RoleName")]
  [MapProperty("Permission.Name", "PermissionName")]
  public partial RolePermissionPreviewDto EntityToPreviewDto(RolePermission entity);

  public partial List<RolePermissionPreviewDto> EntityToPreviewDtoList(List<RolePermission> entities);
}