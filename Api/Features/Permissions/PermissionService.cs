using Api.Core.Repositories;
using Api.Core.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Api.Features.Permissions;

public class PermissionService(
  IPermissionRepository _permissionRepository,
  PermissionMapper _mapper,
  PermissionBusinessRules _businessRules,
  IUnitOfWork _unitOfWork,
  IValidator<CreatePermissionRequest> _createValidator,
  IValidator<UpdatePermissionRequest> _updateValidator,
  ILogger<PermissionService> _logger) : IPermissionService
{
  public async Task<ReturnModel<CreatedPermissionResponseDto>> AddAsync(
    CreatePermissionRequest request,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Yeni yetki oluşturma işlemi başlatıldı. Yetki Adı: {PermissionName}", request.Name);

    var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      _logger.LogWarning("Yetki oluşturma validasyonu başarısız oldu. Yetki Adı: {PermissionName}", request.Name);

      throw new ValidationException(validationResult.Errors);
    }

    await _businessRules.PermissionNameMustBeUniqueAsync(request.Name, cancellationToken: cancellationToken);

    Permission createdPermission = _mapper.CreateToEntity(request);

    await _permissionRepository.AddAsync(createdPermission, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Yetki başarıyla oluşturuldu. ID: {PermissionId}, Yetki Adı: {PermissionName}", createdPermission.Id, createdPermission.Name);

    CreatedPermissionResponseDto response = _mapper.EntityToCreatedResponseDto(createdPermission);

    return new ReturnModel<CreatedPermissionResponseDto>()
    {
      Success = true,
      Message = "Yetki başarıyla oluşturuldu.",
      Data = response,
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<List<PermissionResponseDto>>> GetAllAsync(
    Expression<Func<Permission, bool>>? filter = null,
    Func<IQueryable<Permission>, IQueryable<Permission>>? include = null,
    Func<IQueryable<Permission>, IOrderedQueryable<Permission>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Tüm yetkiler listeleniyor.");

    List<Permission> permissions = await _permissionRepository.GetAllAsync(
      filter: filter,
      include: p => p.Include(p => p.RolePermissions).ThenInclude(rp => rp.Role),
      orderBy: orderBy,
      enableTracking: enableTracking,
      withDeleted: withDeleted,
      cancellationToken: cancellationToken);

    List<PermissionResponseDto> response = _mapper.EntityToResponseDtoList(permissions);

    return new ReturnModel<List<PermissionResponseDto>>()
    {
      Success = true,
      Message = "Yetkiler başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<PermissionResponseDto>> GetAsync(
    Expression<Func<Permission, bool>> predicate,
    Func<IQueryable<Permission>, IQueryable<Permission>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Kriterlere göre yetki sorgulanıyor.");

    var permission = await _permissionRepository.GetAsync(
      predicate: predicate,
      include: p => p.Include(p => p.RolePermissions).ThenInclude(rp => rp.Role),
      enableTracking: enableTracking,
      cancellationToken: cancellationToken);

    if (permission == null)
    {
      _logger.LogWarning("Aranan kriterlere uygun yetki bulunamadı.");

      return new ReturnModel<PermissionResponseDto>()
      {
        Success = false,
        Message = "Yetki bulunamadı.",
        Data = null,
        StatusCode = 200
      };
    }

    var response = _mapper.EntityToResponseDto(permission);

    return new ReturnModel<PermissionResponseDto>
    {
      Success = true,
      Message = "Yetki başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<PermissionResponseDto>> GetByIdAsync(
    Guid id,
    Func<IQueryable<Permission>, IQueryable<Permission>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Yetki detayları getiriliyor. ID: {PermissionId}", id);

    Permission permission = await _businessRules.GetPermissionIfExistAsync(
      id,
      include: p => p.Include(p => p.RolePermissions).ThenInclude(rp => rp.Role),
      enableTracking,
      cancellationToken);

    PermissionResponseDto response = _mapper.EntityToResponseDto(permission);

    return new ReturnModel<PermissionResponseDto>()
    {
      Success = true,
      Message = "Yetki başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> RemoveAsync(
    Guid id,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Yetki silme işlemi başlatıldı. ID: {PermissionId}", id);

    Permission permission = await _businessRules.GetPermissionIfExistAsync(id, enableTracking: true, cancellationToken: cancellationToken);

    _permissionRepository.Delete(permission);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Yetki başarıyla silindi. ID: {PermissionId}", id);

    return new ReturnModel<NoData>
    {
      Success = true,
      Message = "Yetki başarıyla silindi.",
      Data = null,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> UpdateAsync(
    Guid id,
    UpdatePermissionRequest request,
    CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Yetki güncelleme işlemi başlatıldı. ID: {PermissionId}", id);

    var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      _logger.LogWarning("Yetki güncelleme validasyonu başarısız oldu. ID: {PermissionId}", id);

      throw new ValidationException(validationResult.Errors);
    }

    Permission existingPermission = await _businessRules.GetPermissionIfExistAsync(id, enableTracking: true, cancellationToken: cancellationToken);

    if (existingPermission.Name != request.Name)
    {
      await _businessRules.PermissionNameMustBeUniqueAsync(request.Name, cancellationToken: cancellationToken);
    }

    _mapper.UpdateEntityFromRequest(request, existingPermission);

    _permissionRepository.Update(existingPermission);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Yetki başarıyla güncellendi. ID: {PermissionId}, Yeni Adı: {PermissionName}", id, request.Name);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Yetki başarıyla güncellendi.",
      Data = null,
      StatusCode = 200
    };
  }
}
