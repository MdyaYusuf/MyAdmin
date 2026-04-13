using Api.Features.Roles;
using Api.Features.UserRoles;
using Riok.Mapperly.Abstractions;

namespace Api.Features.Users;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class UserMapper
{
  private readonly RoleMapper _roleMapper = new();

  [MapperIgnoreSource(nameof(RegisterUserRequest.Password))]
  [MapperIgnoreTarget(nameof(User.PasswordHash))]
  [MapperIgnoreTarget(nameof(User.PasswordKey))]
  public partial User RegisterToEntity(RegisterUserRequest request);
  [MapperIgnoreTarget(nameof(User.ProfileImageUrl))]
  public partial void UpdateEntityFromRequest(UpdateUserRequest request, User entity);
  [MapProperty(nameof(User.UserRoles), nameof(UserResponseDto.Roles))]
  public partial UserResponseDto EntityToResponseDto(User entity);
  private List<RoleResponseDto> MapUserRolesToRoles(ICollection<UserRole> userRoles)
  {
    if (userRoles == null)
    {
      return new();
    }

    return userRoles
      .Where(ur => ur.Role != null)
      .Select(ur => _roleMapper.EntityToResponseDto(ur.Role))
      .ToList();
  }

  public partial CreatedUserResponseDto EntityToCreatedResponseDto(User entity);

  public partial List<UserResponseDto> EntityToResponseDtoList(List<User> entities);

  public partial UserPreviewDto EntityToPreviewDto(User entity);

  public partial List<UserPreviewDto> EntityToPreviewDtoList(List<User> entities);
}