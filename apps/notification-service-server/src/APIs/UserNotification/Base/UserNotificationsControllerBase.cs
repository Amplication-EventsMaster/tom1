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
    public async Task<ActionResult<UserNotification>> CreateUserNotification(
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
        [FromRoute()] UserNotificationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteUserNotification(uniqueId);
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
    public async Task<ActionResult<List<UserNotification>>> UserNotifications(
        [FromQuery()] UserNotificationFindManyArgs filter
    )
    {
        return Ok(await _service.UserNotifications(filter));
    }

    /// <summary>
    /// Get one UserNotification
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserNotification>> UserNotification(
        [FromRoute()] UserNotificationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.UserNotification(uniqueId);
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
        [FromRoute()] UserNotificationWhereUniqueInput uniqueId,
        [FromQuery()] UserNotificationUpdateInput userNotificationUpdateDto
    )
    {
        try
        {
            await _service.UpdateUserNotification(uniqueId, userNotificationUpdateDto);
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
    public async Task<ActionResult<List<Notification>>> GetNotification(
        [FromRoute()] UserNotificationWhereUniqueInput uniqueId
    )
    {
        var notification = await _service.GetNotification(uniqueId);
        return Ok(notification);
    }

    /// <summary>
    /// Get a User record for UserNotification
    /// </summary>
    [HttpGet("{Id}/users")]
    public async Task<ActionResult<List<User>>> GetUser(
        [FromRoute()] UserNotificationWhereUniqueInput uniqueId
    )
    {
        var user = await _service.GetUser(uniqueId);
        return Ok(user);
    }

    /// <summary>
    /// Meta data about UserNotification records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UserNotificationsMeta(
        [FromQuery()] UserNotificationFindManyArgs filter
    )
    {
        return Ok(await _service.UserNotificationsMeta(filter));
    }
}
