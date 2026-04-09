using Api.Core.Responses;
using System.Linq.Expressions;

namespace Api.Features.UserRoles;

public interface IUserRoleService
{
  Task<ReturnModel<List<UserRoleResponseDto>>> GetAllAsync(
    Expression<Func<UserRole, bool>>? filter = null,
    Func<IQueryable<UserRole>, IQueryable<UserRole>>? include = null,
    Func<IQueryable<UserRole>, IOrderedQueryable<UserRole>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<UserRoleResponseDto>> GetAsync(
    Expression<Func<UserRole, bool>> predicate,
    Func<IQueryable<UserRole>, IQueryable<UserRole>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<UserRoleResponseDto>> GetByIdAsync(
    Guid id,
    Func<IQueryable<UserRole>, IQueryable<UserRole>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<CreatedUserRoleResponseDto>> AddAsync(
    CreateUserRoleRequest request,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> UpdateAsync(
    UpdateUserRoleRequest request,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> RemoveAsync(
    Guid id,
    CancellationToken cancellationToken = default);
}
