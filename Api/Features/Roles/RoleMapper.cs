using Api.Features.Permissions;
using Api.Features.RolePermissions;
using Riok.Mapperly.Abstractions;

namespace Api.Features.Roles;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class RoleMapper
{
  private readonly PermissionMapper _permissionMapper = new();

  public partial Role CreateToEntity(CreateRoleRequest request);

  public partial void UpdateEntityFromRequest(UpdateRoleRequest request, Role entity);

  [MapProperty(nameof(Role.RolePermissions), nameof(RoleResponseDto.Permissions))]
  public partial RoleResponseDto EntityToResponseDto(Role entity);
  private List<PermissionResponseDto> MapRolePermissionsToPermissions(ICollection<RolePermission> rolePermissions)
  {
    if (rolePermissions == null)
    {
      return new();
    }

    return rolePermissions
      .Where(rp => rp.Permission != null)
      .Select(rp => _permissionMapper.EntityToResponseDto(rp.Permission))
      .ToList();
  }

  public partial CreatedRoleResponseDto EntityToCreatedResponseDto(Role entity);

  public partial List<RoleResponseDto> EntityToResponseDtoList(List<Role> entities);

  public partial RolePreviewDto EntityToPreviewDto(Role entity);

  public partial List<RolePreviewDto> EntityToPreviewDtoList(List<Role> entities);
}