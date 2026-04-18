using Api.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Activities;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] 
public class ActivitiesController(IActivityService _activityService) : CustomBaseController
{
  [HttpGet]
  public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
  {
    var result = await _activityService.GetAllAsync(cancellationToken: cancellationToken);

    return CreateActionResult(result);
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetById(
    Guid id,
    CancellationToken cancellationToken)
  {
    var result = await _activityService.GetByIdAsync(id, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpGet("get-by-entity/{entityName}")]
  public async Task<IActionResult> GetByEntity(
    string entityName,
    CancellationToken cancellationToken)
  {
    var result = await _activityService.GetAllAsync(
      filter: a => a.EntityName.ToLower() == entityName.ToLower(),
      cancellationToken: cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPost]
  public async Task<IActionResult> Add(
    [FromBody] CreateActivityRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _activityService.AddAsync(request, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
  {
    var result = await _activityService.RemoveAsync(id, cancellationToken);

    return CreateActionResult(result);
  }
}