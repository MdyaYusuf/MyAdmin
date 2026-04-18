using Api.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Notifications;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController(INotificationService _notificationService) : CustomBaseController
{
  [HttpGet]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
  {
    var result = await _notificationService.GetAllAsync(cancellationToken: cancellationToken);

    return CreateActionResult(result);
  }

  [HttpGet("my-notifications")]
  [Authorize]
  public async Task<IActionResult> GetMyNotifications(CancellationToken cancellationToken)
  {
    var currentUserId = GetUserId();

    var result = await _notificationService.GetAllAsync(
      filter: n => n.UserId == currentUserId,
      cancellationToken: cancellationToken);

    return CreateActionResult(result);
  }

  [HttpGet("{id:guid}")]
  [Authorize]
  public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
  {
    var currentUserId = GetUserId();

    var result = await _notificationService.GetByIdAsync(id, currentUserId, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpGet("unread-count")]
  [Authorize]
  public async Task<IActionResult> GetUnreadCount(CancellationToken cancellationToken)
  {
    var currentUserId = GetUserId();

    var result = await _notificationService.GetUnreadCountAsync(currentUserId, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPost]
  [Authorize(Roles = "Admin")] 
  public async Task<IActionResult> Add([FromBody] CreateNotificationRequest request, CancellationToken cancellationToken)
  {
    var result = await _notificationService.AddAsync(request, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPatch("{id:guid}/mark-as-read")]
  [Authorize]
  public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken cancellationToken)
  {
    var currentUserId = GetUserId();

    var result = await _notificationService.MarkAsReadAsync(id, currentUserId, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpPatch("mark-all-as-read")]
  [Authorize]
  public async Task<IActionResult> MarkAllAsRead(CancellationToken cancellationToken)
  {
    var currentUserId = GetUserId();

    var result = await _notificationService.MarkAllAsReadAsync(currentUserId, cancellationToken);

    return CreateActionResult(result);
  }

  [HttpDelete("{id:guid}")]
  [Authorize]
  public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
  {
    var currentUserId = GetUserId();

    var result = await _notificationService.RemoveAsync(id, currentUserId, cancellationToken);

    return CreateActionResult(result);
  }
}