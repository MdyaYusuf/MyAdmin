using Api.Core.Repositories;
using Api.Core.Responses;
using Api.Features.Roles;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.UserRoles;

public class UserRoleService(
  IUserRoleRepository _userRoleRepository,
  UserRoleBusinessRules _businessRules,
  RoleMapper _roleMapper,
  IUnitOfWork _unitOfWork) : IUserRoleService
{
  public async Task<ReturnModel<NoData>> AssignRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
  {
    await _businessRules.UserRoleRelationMustNotBeDuplicateAsync(userId, roleId, cancellationToken);

    await _userRoleRepository.AddAsync(new UserRole { UserId = userId, RoleId = roleId }, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Rol başarıyla atandı.",
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<NoData>> RevokeRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
  {
    var userRole = await _userRoleRepository.GetAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

    if (userRole == null)
    {
      return new ReturnModel<NoData>()
      {
        Success = false,
        Message = "Kullanıcı, rol ilişkisi bulunamadı.",
        StatusCode = 404
      };
    }

    _userRoleRepository.Delete(userRole);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Rol yetkisi kaldırıldı.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> SyncUserRolesAsync(Guid userId, List<Guid> roleIds, CancellationToken cancellationToken = default)
  {
    var currentRoles = await _userRoleRepository.GetAllAsync(ur => ur.UserId == userId, enableTracking: true);

    // Silinecekler
    var toDelete = currentRoles.Where(ur => !roleIds.Contains(ur.RoleId)).ToList();

    foreach (var ur in toDelete)
    {
      _userRoleRepository.Delete(ur);
    }

    // Eklenecekler
    var toAdd = roleIds
      .Where(id => !currentRoles.Any(ur => ur.RoleId == id))
      .Select(id => new UserRole { UserId = userId, RoleId = id });

    foreach (var ur in toAdd)
    {
      await _userRoleRepository.AddAsync(ur, cancellationToken);
    }

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Kullanıcı rolleri senkronize edildi.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<List<RoleResponseDto>>> GetRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    var userRoles = await _userRoleRepository.GetAllAsync(
      filter: ur => ur.UserId == userId,
      include: q => q.Include(ur => ur.Role),
      cancellationToken: cancellationToken);

    var roles = userRoles
      .Where(ur => ur.Role != null)
      .Select(ur => ur.Role!)
      .ToList();

    List<RoleResponseDto> response = _roleMapper.EntityToResponseDtoList(roles);

    return new ReturnModel<List<RoleResponseDto>>()
    {
      Success = true,
      Message = "Kullanıcı rolleri başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }
}
