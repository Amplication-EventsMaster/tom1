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
    public async Task<ActionResult<UserDto>> CreateUser(UserCreateInput input)
    {
        var user = await _service.CreateUser(input);

        return CreatedAtAction(nameof(User), new { id = user.Id }, user);
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUser([FromRoute()] UserIdDto idDto)
    {
        try
        {
            await _service.DeleteUser(idDto);
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
    public async Task<ActionResult<List<UserDto>>> Users([FromQuery()] UserFindMany filter)
    {
        return Ok(await _service.Users(filter));
    }

    /// <summary>
    /// Get one User
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserDto>> User([FromRoute()] UserIdDto idDto)
    {
        try
        {
            return await _service.User(idDto);
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
        [FromRoute()] UserIdDto idDto,
        [FromQuery()] UserUpdateInput userUpdateDto
    )
    {
        try
        {
            await _service.UpdateUser(idDto, userUpdateDto);
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
        [FromRoute()] UserIdDto idDto,
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
    /// Disconnect multiple UserNotifications records from User
    /// </summary>
    [HttpDelete("{Id}/userNotifications")]
    public async Task<ActionResult> DisconnectUserNotifications(
        [FromRoute()] UserIdDto idDto,
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
    /// Find multiple UserNotifications records for User
    /// </summary>
    [HttpGet("{Id}/userNotifications")]
    public async Task<ActionResult<List<UserNotificationDto>>> FindUserNotifications(
        [FromRoute()] UserIdDto idDto,
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
    /// Meta data about User records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UsersMeta([FromQuery()] UserFindMany filter)
    {
        return Ok(await _service.UsersMeta(filter));
    }

    /// <summary>
    /// Update multiple UserNotifications records for User
    /// </summary>
    [HttpPatch("{Id}/userNotifications")]
    public async Task<ActionResult> UpdateUserNotifications(
        [FromRoute()] UserIdDto idDto,
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
}
