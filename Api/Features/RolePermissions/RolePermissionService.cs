using Api.Core.Responses;
using System.Linq.Expressions;

namespace Api.Features.RolePermissions;

public class RolePermissionService : IRolePermissionService
{
  public Task<ReturnModel<CreatedRolePermissionResponseDto>> AddAsync(CreateRolePermissionRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<List<RolePermissionResponseDto>>> GetAllAsync(Expression<Func<RolePermission, bool>>? filter = null, Func<IQueryable<RolePermission>, IQueryable<RolePermission>>? include = null, Func<IQueryable<RolePermission>, IOrderedQueryable<RolePermission>>? orderBy = null, bool enableTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<RolePermissionResponseDto>> GetAsync(Expression<Func<RolePermission, bool>> predicate, Func<IQueryable<RolePermission>, IQueryable<RolePermission>>? include = null, bool enableTracking = false, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<RolePermissionResponseDto>> GetByIdAsync(Guid id, Func<IQueryable<RolePermission>, IQueryable<RolePermission>>? include = null, bool enableTracking = false, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<NoData>> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<NoData>> UpdateAsync(UpdateRolePermissionRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
