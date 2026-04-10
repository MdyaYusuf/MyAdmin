using Api.Core.Responses;
using System.Linq.Expressions;

namespace Api.Features.Roles;

public class RoleService : IRoleService
{
  public Task<ReturnModel<RoleResponseDto>> AddAsync(CreateRoleRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<List<RoleResponseDto>>> GetAllAsync(Expression<Func<Role, bool>>? filter = null, Func<IQueryable<Role>, IQueryable<Role>>? include = null, Func<IQueryable<Role>, IOrderedQueryable<Role>>? orderBy = null, bool enableTracking = false, bool withDeleted = false, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<RoleResponseDto>> GetAsync(Expression<Func<Role, bool>> predicate, Func<IQueryable<Role>, IQueryable<Role>>? include = null, bool enableTracking = false, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<RoleResponseDto>> GetByIdAsync(int id, Func<IQueryable<Role>, IQueryable<Role>>? include = null, bool enableTracking = false, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<NoData>> RemoveAsync(int id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<NoData>> UpdateAsync(Guid id, UpdateRoleRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
