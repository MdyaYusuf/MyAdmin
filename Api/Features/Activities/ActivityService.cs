using Api.Core.Repositories;
using Api.Core.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Features.Activities;

public class ActivityService(
  IActivityRepository _activityRepository,
  ActivityMapper _mapper,
  ActivityBusinessRules _businessRules,
  IUnitOfWork _unitOfWork,
  IValidator<CreateActivityRequest> _createValidator) : IActivityService
{
  public async Task<ReturnModel<NoData>> AddAsync(CreateActivityRequest request, CancellationToken cancellationToken = default)
  {
    var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    Activity createdActivity = _mapper.CreateToEntity(request);

    await _activityRepository.AddAsync(createdActivity, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Aktivite kaydı başarıyla oluşturuldu.",
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<List<ActivityResponseDto>>> GetAllAsync(
    Expression<Func<Activity, bool>>? filter = null,
    Func<IQueryable<Activity>, IQueryable<Activity>>? include = null,
    Func<IQueryable<Activity>, IOrderedQueryable<Activity>>? orderBy = null,
    bool enableTracking = false,
    bool withDeleted = false,
    CancellationToken cancellationToken = default)
  {
    List<Activity> activities = await _activityRepository.GetAllAsync(
      filter: filter,
      include: a => a.Include(a => a.User),
      orderBy: orderBy,
      enableTracking: enableTracking,
      withDeleted: withDeleted,
      cancellationToken: cancellationToken);

    List<ActivityResponseDto> response = _mapper.EntityToResponseDtoList(activities);

    return new ReturnModel<List<ActivityResponseDto>>()
    {
      Success = true,
      Message = "Aktivite listesi başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<ActivityResponseDto>> GetAsync(
    Expression<Func<Activity, bool>> predicate,
    Func<IQueryable<Activity>, IQueryable<Activity>>? include = null,
    bool enableTracking = false,
    CancellationToken cancellationToken = default)
  {
    var activity = await _activityRepository.GetAsync(
      predicate: predicate,
      include: a => a.Include(a => a.User),
      enableTracking: enableTracking,
      cancellationToken: cancellationToken);

    if (activity == null)
    {
      return new ReturnModel<ActivityResponseDto>()
      {
        Success = false,
        Message = "Aktivite bulunamadı.",
        StatusCode = 200
      };
    }

    ActivityResponseDto response = _mapper.EntityToResponseDto(activity);

    return new ReturnModel<ActivityResponseDto>()
    {
      Success = true,
      Message = "Aktivite başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<ActivityResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    Activity activity = await _businessRules.GetActivityIfExistAsync(id);

    ActivityResponseDto response = _mapper.EntityToResponseDto(activity);

    return new ReturnModel<ActivityResponseDto>()
    {
      Success = true,
      Message = "Aktivite detayları başarıyla getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
  {
    Activity activity = await _businessRules.GetActivityIfExistAsync(id);

    _activityRepository.Delete(activity);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Aktivite kaydı silindi.",
      StatusCode = 200
    };
  }
}