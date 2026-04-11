using Api.Core.Repositories;
using Api.Core.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Features.Roles;

public class RoleService(
    IRoleRepository _roleRepository,
    RoleMapper _mapper,
    RoleBusinessRules _businessRules,
    IUnitOfWork _unitOfWork,
    IValidator<CreateRoleRequest> _createValidator,
    IValidator<UpdateRoleRequest> _updateValidator) : IRoleService
{
  public async Task<ReturnModel<RoleResponseDto>> AddAsync(CreateRoleRequest request, CancellationToken cancellationToken = default)
  {

    var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    await _businessRules.RoleNameMustBeUniqueAsync(request.Name, cancellationToken: cancellationToken);

    Role role = _mapper.CreateToEntity(request);

    await _roleRepository.AddAsync(role, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    var response = _mapper.EntityToResponseDto(role);

    return new ReturnModel<RoleResponseDto>
    {
      Success = true,
      Message = "Rol başarıyla oluşturuldu.",
      Data = response,
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<List<RoleResponseDto>>> GetAllAsync(
    Expression<Func<Role, bool>>? filter = null,
    Func<IQueryable<Role>, IQueryable<Role>>? include = null,
    Func<IQueryable<Role>, IOrderedQueryable<Role>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default)
  {
    List<Role> roles = await _roleRepository.GetAllAsync(
       filter: filter,
       include: r => r.Include(r => r.RolePermissions).ThenInclude(rp => rp.Permission),
       orderBy: orderBy,
       enableTracking: enableTracking,
       withDeleted: withDeleted);

    List<RoleResponseDto> response = _mapper.EntityToResponseDtoList(roles);

    return new ReturnModel<List<RoleResponseDto>>
    {
      Success = true,
      Message = "Roller başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<RoleResponseDto>> GetAsync(
    Expression<Func<Role, bool>> predicate,
    Func<IQueryable<Role>, IQueryable<Role>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default)
  {
    var role = await _roleRepository.GetAsync(
      predicate: predicate,
      include: r => r.Include(r => r.RolePermissions).ThenInclude(rp => rp.Permission),
      enableTracking: enableTracking,
      cancellationToken: cancellationToken);

    if (role == null)
    {
      return new ReturnModel<RoleResponseDto>()
      {
        Success = false,
        Message = "Rol bulunamadı.",
        Data = null,
        StatusCode = 200
      };
    }

    var roleResponse = _mapper.EntityToResponseDto(role);

    return new ReturnModel<RoleResponseDto>()
    {
      Success = true,
      Message = "Rol başarıyla getirildi.",
      Data = roleResponse,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<RoleResponseDto>> GetByIdAsync(Guid id, Func<IQueryable<Role>, IQueryable<Role>>? include = null, bool enableTracking = false, CancellationToken cancellationToken = default)
  {
    Role role = await _businessRules.GetRoleIfExistAsync(id, include, enableTracking, cancellationToken);
    var roleResponse = _mapper.EntityToResponseDto(role);

    return new ReturnModel<RoleResponseDto>()
    {
      Success = true,
      Message = "Rol başarıyla getirildi.",
      Data = roleResponse,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
  {
    var role = await _businessRules.GetRoleIfExistAsync(id, enableTracking: true, cancellationToken: cancellationToken);

    _roleRepository.Delete(role);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>
    {
      Success = true,
      Message = "Rol başarıyla silindi.",
      Data = null,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> UpdateAsync(Guid id, UpdateRoleRequest request, CancellationToken cancellationToken = default)
  {
    var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    var role = await _businessRules.GetRoleIfExistAsync(id, enableTracking: true, cancellationToken: cancellationToken);

    _mapper.UpdateEntityFromRequest(request, role);

    _roleRepository.Update(role);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>
    {
      Success = true,
      Message = "Rol başarıyla güncellendi.",
      Data = null,
      StatusCode = 200
    };
  }
}