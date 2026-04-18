using Api.Core.Responses;
using System.Linq.Expressions;

namespace Api.Features.Activities;

public interface IActivityService
{
  Task<ReturnModel<List<ActivityResponseDto>>> GetAllAsync(
    Expression<Func<Activity, bool>>? filter = null,
    Func<IQueryable<Activity>, IQueryable<Activity>>? include = null,
    Func<IQueryable<Activity>, IOrderedQueryable<Activity>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<ActivityResponseDto>> GetAsync(
    Expression<Func<Activity, bool>> predicate,
    Func<IQueryable<Activity>, IQueryable<Activity>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<ActivityResponseDto>> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> AddAsync(
    CreateActivityRequest request,
    CancellationToken cancellationToken = default);

  Task<ReturnModel<NoData>> RemoveAsync(
    Guid id,
    CancellationToken cancellationToken = default);
}