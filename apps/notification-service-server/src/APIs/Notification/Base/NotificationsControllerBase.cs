using Microsoft.AspNetCore.Mvc;
using NotificationService.APIs;
using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;
using NotificationService.APIs.Errors;

namespace NotificationService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class NotificationsControllerBase : ControllerBase
{
    protected readonly INotificationsService _service;

    public NotificationsControllerBase(INotificationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Notification
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<NotificationDto>> CreateNotification(
        NotificationCreateInput input
    )
    {
        var notification = await _service.CreateNotification(input);

        return CreatedAtAction(nameof(Notification), new { id = notification.Id }, notification);
    }

    /// <summary>
    /// Delete one Notification
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteNotification([FromRoute()] NotificationIdDto idDto)
    {
        try
        {
            await _service.DeleteNotification(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Notifications
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<NotificationDto>>> Notifications(
        [FromQuery()] NotificationFindMany filter
    )
    {
        return Ok(await _service.Notifications(filter));
    }

    /// <summary>
    /// Get one Notification
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<NotificationDto>> Notification(
        [FromRoute()] NotificationIdDto idDto
    )
    {
        try
        {
            return await _service.Notification(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple UserNotifications records to Notification
    /// </summary>
    [HttpPost("{Id}/userNotifications")]
    public async Task<ActionResult> ConnectUserNotifications(
        [FromRoute()] NotificationIdDto idDto,
        [FromQuery()] UserNotificationIdDto[] userNotificationsId
    )
    {
        try
        {
            await _service.ConnectUserNotifications(idDto, userNotificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple UserNotifications records from Notification
    /// </summary>
    [HttpDelete("{Id}/userNotifications")]
    public async Task<ActionResult> DisconnectUserNotifications(
        [FromRoute()] NotificationIdDto idDto,
        [FromBody()] UserNotificationIdDto[] userNotificationsId
    )
    {
        try
        {
            await _service.DisconnectUserNotifications(idDto, userNotificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple UserNotifications records for Notification
    /// </summary>
    [HttpGet("{Id}/userNotifications")]
    public async Task<ActionResult<List<UserNotificationDto>>> FindUserNotifications(
        [FromRoute()] NotificationIdDto idDto,
        [FromQuery()] UserNotificationFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindUserNotifications(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a NotificationType record for Notification
    /// </summary>
    [HttpGet("{Id}/notificationTypes")]
    public async Task<ActionResult<List<NotificationTypeDto>>> GetNotificationType(
        [FromRoute()] NotificationIdDto idDto
    )
    {
        var notificationType = await _service.GetNotificationType(idDto);
        return Ok(notificationType);
    }

    /// <summary>
    /// Meta data about Notification records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> NotificationsMeta(
        [FromQuery()] NotificationFindMany filter
    )
    {
        return Ok(await _service.NotificationsMeta(filter));
    }

    /// <summary>
    /// Update multiple UserNotifications records for Notification
    /// </summary>
    [HttpPatch("{Id}/userNotifications")]
    public async Task<ActionResult> UpdateUserNotifications(
        [FromRoute()] NotificationIdDto idDto,
        [FromBody()] UserNotificationIdDto[] userNotificationsId
    )
    {
        try
        {
            await _service.UpdateUserNotifications(idDto, userNotificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one Notification
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateNotification(
        [FromRoute()] NotificationIdDto idDto,
        [FromQuery()] NotificationUpdateInput notificationUpdateDto
    )
    {
        try
        {
            await _service.UpdateNotification(idDto, notificationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
