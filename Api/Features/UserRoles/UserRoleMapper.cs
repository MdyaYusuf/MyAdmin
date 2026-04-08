using Riok.Mapperly.Abstractions;

namespace Api.Features.UserRoles;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class UserRoleMapper
{
  public partial UserRole CreateToEntity(CreateUserRoleRequest request);

  public partial void UpdateEntityFromRequest(UpdateUserRoleRequest request, UserRole entity);

  [MapProperty("User.Username", nameof(UserRoleResponseDto.Username))]
  [MapProperty("Role.Name", nameof(UserRoleResponseDto.RoleName))]
  public partial UserRoleResponseDto EntityToResponseDto(UserRole entity);

  public partial CreatedUserRoleResponseDto EntityToCreatedResponseDto(UserRole entity);

  public partial List<UserRoleResponseDto> EntityToResponseDtoList(List<UserRole> entities);

  [MapProperty("User.Username", "Username")]
  [MapProperty("Role.Name", "RoleName")]
  public partial UserRolePreviewDto EntityToPreviewDto(UserRole entity);

  public partial List<UserRolePreviewDto> EntityToPreviewDtoList(List<UserRole> entities);
}