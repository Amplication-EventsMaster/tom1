using Microsoft.AspNetCore.Mvc;
using NotificationService.APIs;
using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;
using NotificationService.APIs.Errors;

namespace NotificationService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class NotificationTypesControllerBase : ControllerBase
{
    protected readonly INotificationTypesService _service;

    public NotificationTypesControllerBase(INotificationTypesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one NotificationType
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<NotificationTypeDto>> CreateNotificationType(
        NotificationTypeCreateInput input
    )
    {
        var notificationType = await _service.CreateNotificationType(input);

        return CreatedAtAction(
            nameof(NotificationType),
            new { id = notificationType.Id },
            notificationType
        );
    }

    /// <summary>
    /// Delete one NotificationType
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteNotificationType(
        [FromRoute()] NotificationTypeIdDto idDto
    )
    {
        try
        {
            await _service.DeleteNotificationType(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many NotificationTypes
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<NotificationTypeDto>>> NotificationTypes(
        [FromQuery()] NotificationTypeFindMany filter
    )
    {
        return Ok(await _service.NotificationTypes(filter));
    }

    /// <summary>
    /// Get one NotificationType
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<NotificationTypeDto>> NotificationType(
        [FromRoute()] NotificationTypeIdDto idDto
    )
    {
        try
        {
            return await _service.NotificationType(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Notifications records to NotificationType
    /// </summary>
    [HttpPost("{Id}/notifications")]
    public async Task<ActionResult> ConnectNotifications(
        [FromRoute()] NotificationTypeIdDto idDto,
        [FromQuery()] NotificationIdDto[] notificationsId
    )
    {
        try
        {
            await _service.ConnectNotifications(idDto, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Notifications records from NotificationType
    /// </summary>
    [HttpDelete("{Id}/notifications")]
    public async Task<ActionResult> DisconnectNotifications(
        [FromRoute()] NotificationTypeIdDto idDto,
        [FromBody()] NotificationIdDto[] notificationsId
    )
    {
        try
        {
            await _service.DisconnectNotifications(idDto, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Notifications records for NotificationType
    /// </summary>
    [HttpGet("{Id}/notifications")]
    public async Task<ActionResult<List<NotificationDto>>> FindNotifications(
        [FromRoute()] NotificationTypeIdDto idDto,
        [FromQuery()] NotificationFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindNotifications(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about NotificationType records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> NotificationTypesMeta(
        [FromQuery()] NotificationTypeFindMany filter
    )
    {
        return Ok(await _service.NotificationTypesMeta(filter));
    }

    /// <summary>
    /// Update multiple Notifications records for NotificationType
    /// </summary>
    [HttpPatch("{Id}/notifications")]
    public async Task<ActionResult> UpdateNotifications(
        [FromRoute()] NotificationTypeIdDto idDto,
        [FromBody()] NotificationIdDto[] notificationsId
    )
    {
        try
        {
            await _service.UpdateNotifications(idDto, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one NotificationType
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateNotificationType(
        [FromRoute()] NotificationTypeIdDto idDto,
        [FromQuery()] NotificationTypeUpdateInput notificationTypeUpdateDto
    )
    {
        try
        {
            await _service.UpdateNotificationType(idDto, notificationTypeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
