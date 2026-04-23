using Api.Core.Repositories;
using Api.Core.Responses;
using Api.Features.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Features.UserRoles;

public class UserRoleService(
  IUserRoleRepository _userRoleRepository,
  IRoleRepository _roleRepository,
  UserRoleBusinessRules _businessRules,
  RoleMapper _roleMapper,
  IUnitOfWork _unitOfWork,
  ILogger<UserRoleService> _logger) : IUserRoleService
{
  public async Task<ReturnModel<NoData>> AssignRoleAsync(
    Guid userId,
    Guid roleId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Kullanıcıya rol atama işlemi başlatıldı. Kullanıcı ID: {UserId}, Rol ID: {RoleId}", userId, roleId);

    await _businessRules.UserRoleRelationMustNotBeDuplicateAsync(userId, roleId, cancellationToken);

    await _userRoleRepository.AddAsync(new UserRole { UserId = userId, RoleId = roleId }, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Rol başarıyla atandı. Kullanıcı ID: {UserId}, Rol ID: {RoleId}", userId, roleId);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Rol başarıyla atandı.",
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<NoData>> RevokeRoleAsync(
    Guid userId,
    Guid roleId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Kullanıcının rolü kaldırılıyor. Kullanıcı ID: {UserId}, Rol ID: {RoleId}", userId, roleId);

    var userRole = await _userRoleRepository.GetAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

    if (userRole == null)
    {
      _logger.LogWarning("Rol kaldırma işlemi başarısız: İlişki bulunamadı. Kullanıcı ID: {UserId}, Rol ID: {RoleId}", userId, roleId);

      return new ReturnModel<NoData>()
      {
        Success = false,
        Message = "Kullanıcı, rol ilişkisi bulunamadı.",
        StatusCode = 404
      };
    }

    _userRoleRepository.Delete(userRole);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Rol yetkisi başarıyla kaldırıldı. Kullanıcı ID: {UserId}, Rol ID: {RoleId}", userId, roleId);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Rol yetkisi kaldırıldı.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> SyncUserRolesAsync(
    Guid userId,
    List<Guid> roleIds,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Kullanıcı rolleri senkronize ediliyor. Kullanıcı ID: {UserId}, Hedef Rol Sayısı: {TargetCount}", userId, roleIds.Count);

    var currentRoles = await _userRoleRepository.GetAllAsync(
      filter: ur => ur.UserId == userId,
      enableTracking: true,
      cancellationToken: cancellationToken);

    var currentRoleIds = currentRoles.Select(ur => ur.RoleId).ToHashSet();
    var targetRoleIds = roleIds.ToHashSet();

    var toDelete = currentRoles.Where(ur => !targetRoleIds.Contains(ur.RoleId)).ToList();

    foreach (var ur in toDelete)
    {
      _userRoleRepository.Delete(ur);
    }

    var idsToAdd = roleIds.Where(id => !currentRoleIds.Contains(id)).ToList();

    foreach (var rId in idsToAdd)
    {
      await _userRoleRepository.AddAsync(new UserRole { UserId = userId, RoleId = rId }, cancellationToken);
    }

    if (toDelete.Any() || idsToAdd.Any())
    {
      _logger.LogInformation("Kullanıcı rolleri senkronizasyonu tamamlandı. Kullanıcı ID: {UserId}. Eklenen: {AddCount}, Silinen: {DeleteCount}",
          userId, idsToAdd.Count, toDelete.Count);

      await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    else
    {
      _logger.LogInformation("Kullanıcı rolleri zaten güncel, değişiklik yapılmadı. Kullanıcı ID: {UserId}", userId);
    }

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Kullanıcı rolleri başarıyla senkronize edildi.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<List<RoleResponseDto>>> GetRolesByUserIdAsync(
    Guid userId,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Kullanıcıya ait roller listeleniyor. Kullanıcı ID: {UserId}", userId);

    var roles = await _roleRepository
      .Query(enableTracking: false)
      .Where(r => r.UserRoles.Any(ur => ur.UserId == userId))
      .ToListAsync(cancellationToken);

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
