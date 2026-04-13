using Api.Core.Responses;
using System.Linq.Expressions;

namespace Api.Features.Permissions;

public interface IPermissionService
{
  Task<ReturnModel<List<PermissionResponseDto>>> GetAllAsync(
    Expression<Func<Permission, bool>>? filter = null,
    Func<IQueryable<Permission>, IQueryable<Permission>>? include = null,
    Func<IQueryable<Permission>, IOrderedQueryable<Permission>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<PermissionResponseDto>> GetAsync(
    Expression<Func<Permission, bool>> predicate,
    Func<IQueryable<Permission>, IQueryable<Permission>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<PermissionResponseDto>> GetByIdAsync(
    Guid id,
    Func<IQueryable<Permission>, IQueryable<Permission>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<CreatedPermissionResponseDto>> AddAsync(
    CreatePermissionRequest request,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> UpdateAsync(
    Guid id,
    UpdatePermissionRequest request,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> RemoveAsync(
    Guid id,
    CancellationToken cancellationToken = default);
}
