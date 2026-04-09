using Api.Core.Responses;
using System.Linq.Expressions;

namespace Api.Features.RolePermissions;

public interface IRolePermissionService
{
  Task<ReturnModel<List<RolePermissionResponseDto>>> GetAllAsync(
    Expression<Func<RolePermission, bool>>? filter = null,
    Func<IQueryable<RolePermission>, IQueryable<RolePermission>>? include = null,
    Func<IQueryable<RolePermission>, IOrderedQueryable<RolePermission>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<RolePermissionResponseDto>> GetAsync(
    Expression<Func<RolePermission, bool>> predicate,
    Func<IQueryable<RolePermission>, IQueryable<RolePermission>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<RolePermissionResponseDto>> GetByIdAsync(
    Guid id,
    Func<IQueryable<RolePermission>, IQueryable<RolePermission>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<CreatedRolePermissionResponseDto>> AddAsync(
    CreateRolePermissionRequest request,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> UpdateAsync(
    UpdateRolePermissionRequest request,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> RemoveAsync(
    Guid id,
    CancellationToken cancellationToken = default);
}
