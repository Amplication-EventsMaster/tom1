using Microsoft.AspNetCore.Mvc;
using NotificationService.APIs;
using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;
using NotificationService.APIs.Errors;

namespace NotificationService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UsersControllerBase : ControllerBase
{
    protected readonly IUsersService _service;

    public UsersControllerBase(IUsersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<User>> CreateUser(UserCreateInput input)
    {
        var user = await _service.CreateUser(input);

        return CreatedAtAction(nameof(User), new { id = user.Id }, user);
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUser([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteUser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<User>>> Users([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.Users(filter));
    }

    /// <summary>
    /// Get one User
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<User>> User([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.User(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one User
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateUser(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] UserUpdateInput userUpdateDto
    )
    {
        try
        {
            await _service.UpdateUser(uniqueId, userUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple UserNotifications records to User
    /// </summary>
    [HttpPost("{Id}/userNotifications")]
    public async Task<ActionResult> ConnectUserNotifications(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] UserNotificationWhereUniqueInput[] userNotificationsId
    )
    {
        try
        {
            await _service.ConnectUserNotifications(uniqueId, userNotificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple UserNotifications records from User
    /// </summary>
    [HttpDelete("{Id}/userNotifications")]
    public async Task<ActionResult> DisconnectUserNotifications(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] UserNotificationWhereUniqueInput[] userNotificationsId
    )
    {
        try
        {
            await _service.DisconnectUserNotifications(uniqueId, userNotificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple UserNotifications records for User
    /// </summary>
    [HttpGet("{Id}/userNotifications")]
    public async Task<ActionResult<List<UserNotification>>> FindUserNotifications(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] UserNotificationFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindUserNotifications(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UsersMeta([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.UsersMeta(filter));
    }

    /// <summary>
    /// Update multiple UserNotifications records for User
    /// </summary>
    [HttpPatch("{Id}/userNotifications")]
    public async Task<ActionResult> UpdateUserNotifications(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] UserNotificationWhereUniqueInput[] userNotificationsId
    )
    {
        try
        {
            await _service.UpdateUserNotifications(uniqueId, userNotificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
