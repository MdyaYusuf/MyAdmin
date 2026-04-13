using Api.Core.Exceptions;

namespace Api.Features.UserRoles;

public class UserRoleBusinessRules(IUserRoleRepository _userRoleRepository)
{
  public async Task<UserRole> GetUserRoleIfExistAsync(Guid id, CancellationToken cancellationToken = default)
  {
    var ur = await _userRoleRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

    if (ur == null)
    {
      throw new NotFoundException("Kullanıcı, rol ilişkisi bulunamadı.");
    }

    return ur;
  }

  public async Task UserRoleRelationMustNotBeDuplicateAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
  {
    var exists = await _userRoleRepository.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

    if (exists)
    {
      throw new BusinessException("Kullanıcı zaten bu role sahip.");
    }
  }
}
