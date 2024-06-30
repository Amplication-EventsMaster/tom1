using Microsoft.AspNetCore.Mvc;
using NotificationService.APIs;
using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;
using NotificationService.APIs.Errors;

namespace NotificationService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UserNotificationsControllerBase : ControllerBase
{
    protected readonly IUserNotificationsService _service;

    public UserNotificationsControllerBase(IUserNotificationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one UserNotification
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<UserNotificationDto>> CreateUserNotification(
        UserNotificationCreateInput input
    )
    {
        var userNotification = await _service.CreateUserNotification(input);

        return CreatedAtAction(
            nameof(UserNotification),
            new { id = userNotification.Id },
            userNotification
        );
    }

    /// <summary>
    /// Delete one UserNotification
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUserNotification(
        [FromRoute()] UserNotificationIdDto idDto
    )
    {
        try
        {
            await _service.DeleteUserNotification(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many UserNotifications
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<UserNotificationDto>>> UserNotifications(
        [FromQuery()] UserNotificationFindMany filter
    )
    {
        return Ok(await _service.UserNotifications(filter));
    }

    /// <summary>
    /// Get one UserNotification
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserNotificationDto>> UserNotification(
        [FromRoute()] UserNotificationIdDto idDto
    )
    {
        try
        {
            return await _service.UserNotification(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one UserNotification
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateUserNotification(
        [FromRoute()] UserNotificationIdDto idDto,
        [FromQuery()] UserNotificationUpdateInput userNotificationUpdateDto
    )
    {
        try
        {
            await _service.UpdateUserNotification(idDto, userNotificationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Notification record for UserNotification
    /// </summary>
    [HttpGet("{Id}/notifications")]
    public async Task<ActionResult<List<NotificationDto>>> GetNotification(
        [FromRoute()] UserNotificationIdDto idDto
    )
    {
        var notification = await _service.GetNotification(idDto);
        return Ok(notification);
    }

    /// <summary>
    /// Get a User record for UserNotification
    /// </summary>
    [HttpGet("{Id}/users")]
    public async Task<ActionResult<List<UserDto>>> GetUser(
        [FromRoute()] UserNotificationIdDto idDto
    )
    {
        var user = await _service.GetUser(idDto);
        return Ok(user);
    }

    /// <summary>
    /// Meta data about UserNotification records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UserNotificationsMeta(
        [FromQuery()] UserNotificationFindMany filter
    )
    {
        return Ok(await _service.UserNotificationsMeta(filter));
    }
}
