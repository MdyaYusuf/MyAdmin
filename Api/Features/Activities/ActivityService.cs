using Api.Core.Repositories;
using Api.Core.Responses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Api.Features.Activities;

public class ActivityService(
  IActivityRepository _activityRepository,
  ActivityMapper _mapper,
  ActivityBusinessRules _businessRules,
  IUnitOfWork _unitOfWork,
  IValidator<CreateActivityRequest> _createValidator,
  ILogger<ActivityService> _logger) : IActivityService
{
  public async Task<ReturnModel<NoData>> AddAsync(CreateActivityRequest request, CancellationToken cancellationToken = default)
  {
    _logger.LogInformation("Yeni aktivite kaydı oluşturma işlemi başlatıldı.");

    var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);

    if (!validationResult.IsValid)
    {
      _logger.LogWarning("Aktivite validasyonu başarısız oldu.");
      throw new ValidationException(validationResult.Errors);
    }

    Activity createdActivity = _mapper.CreateToEntity(request);

    await _activityRepository.AddAsync(createdActivity, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Aktivite başarıyla kaydedildi. ID: {ActivityId}", createdActivity.Id);

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
    _logger.LogInformation("Tüm aktiviteler listeleniyor.");

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
    _logger.LogInformation("Belirli kriterlere göre aktivite sorgulanıyor.");

    var activity = await _activityRepository.GetAsync(
      predicate: predicate,
      include: a => a.Include(a => a.User),
      enableTracking: enableTracking,
      cancellationToken: cancellationToken);

    if (activity == null)
    {
      _logger.LogWarning("Aranan kriterlere uygun aktivite bulunamadı.");

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
    _logger.LogInformation("Aktivite detayları getiriliyor. ID: {ActivityId}", id);

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
    _logger.LogInformation("Aktivite silme işlemi başlatıldı. ID: {ActivityId}", id);

    Activity activity = await _businessRules.GetActivityIfExistAsync(id);

    _activityRepository.Delete(activity);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _logger.LogInformation("Aktivite başarıyla silindi. ID: {ActivityId}", id);

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Aktivite kaydı silindi.",
      StatusCode = 200
    };
  }
}