using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;

namespace NotificationService.APIs;

public interface IUsersService
{
    /// <summary>
    /// Create one User
    /// </summary>
    public Task<UserDto> CreateUser(UserCreateInput userDto);

    /// <summary>
    /// Delete one User
    /// </summary>
    public Task DeleteUser(UserIdDto idDto);

    /// <summary>
    /// Find many Users
    /// </summary>
    public Task<List<UserDto>> Users(UserFindMany findManyArgs);

    /// <summary>
    /// Get one User
    /// </summary>
    public Task<UserDto> User(UserIdDto idDto);

    /// <summary>
    /// Update one User
    /// </summary>
    public Task UpdateUser(UserIdDto idDto, UserUpdateInput updateDto);

    /// <summary>
    /// Connect multiple UserNotifications records to User
    /// </summary>
    public Task ConnectUserNotifications(
        UserIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    );

    /// <summary>
    /// Disconnect multiple UserNotifications records from User
    /// </summary>
    public Task DisconnectUserNotifications(
        UserIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    );

    /// <summary>
    /// Find multiple UserNotifications records for User
    /// </summary>
    public Task<List<UserNotificationDto>> FindUserNotifications(
        UserIdDto idDto,
        UserNotificationFindMany UserNotificationFindMany
    );

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public Task<MetadataDto> UsersMeta(UserFindMany findManyArgs);

    /// <summary>
    /// Update multiple UserNotifications records for User
    /// </summary>
    public Task UpdateUserNotifications(
        UserIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    );
}
