using Riok.Mapperly.Abstractions;

namespace Api.Features.Users;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class UserMapper
{
  [MapperIgnoreSource(nameof(CreateUserRequest.Password))]
  [MapperIgnoreTarget(nameof(User.PasswordHash))]
  [MapperIgnoreTarget(nameof(User.PasswordKey))]
  public partial User CreateToEntity(CreateUserRequest request);

  public partial void UpdateEntityFromRequest(UpdateUserRequest request, User entity);

  public partial UserResponseDto EntityToResponseDto(User entity);

  public partial CreatedUserResponseDto EntityToCreatedResponseDto(User entity);

  public partial List<UserResponseDto> EntityToResponseDtoList(List<User> entities);

  public partial UserPreviewDto EntityToPreviewDto(User entity);

  public partial List<UserPreviewDto> EntityToPreviewDtoList(List<User> entities);
}