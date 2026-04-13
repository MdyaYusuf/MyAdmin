using Api.Core.Repositories;
using Api.Core.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Features.Permissions;

public class PermissionService(
  IPermissionRepository _permissionRepository,
  PermissionMapper _mapper,
  PermissionBusinessRules _businessRules,
  IUnitOfWork _unitOfWork,
  IValidator<CreatePermissionRequest> _createValidator,
  IValidator<UpdatePermissionRequest> _updateValidator) : IPermissionService
{
  public async Task<ReturnModel<CreatedPermissionResponseDto>> AddAsync(
    CreatePermissionRequest request,
    CancellationToken cancellationToken = default)
  {
    var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    await _businessRules.PermissionNameMustBeUniqueAsync(request.Name, cancellationToken: cancellationToken);

    Permission createdPermission = _mapper.CreateToEntity(request);

    await _permissionRepository.AddAsync(createdPermission, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

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
    var permission = await _permissionRepository.GetAsync(
      predicate: predicate,
      include: p => p.Include(p => p.RolePermissions).ThenInclude(rp => rp.Role),
      enableTracking: enableTracking,
      cancellationToken: cancellationToken);

    if (permission == null)
    {
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

  public async Task<ReturnModel<NoData>> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
  {
    Permission permission = await _businessRules.GetPermissionIfExistAsync(id, enableTracking: true, cancellationToken: cancellationToken);

    _permissionRepository.Delete(permission);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>
    {
      Success = true,
      Message = "Yetki başarıyla silindi.",
      Data = null,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> UpdateAsync(Guid id, UpdatePermissionRequest request, CancellationToken cancellationToken = default)
  {
    var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
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

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Yetki başarıyla güncellendi.",
      Data = null,
      StatusCode = 200
    };
  }
}
