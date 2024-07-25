using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;

namespace NotificationService.APIs;

public interface IUsersService
{
    /// <summary>
    /// Create one User
    /// </summary>
    public Task<User> CreateUser(UserCreateInput user);

    /// <summary>
    /// Delete one User
    /// </summary>
    public Task DeleteUser(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Users
    /// </summary>
    public Task<List<User>> Users(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Get one User
    /// </summary>
    public Task<User> User(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one User
    /// </summary>
    public Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto);

    /// <summary>
    /// Connect multiple UserNotifications records to User
    /// </summary>
    public Task ConnectUserNotifications(
        UserWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    );

    /// <summary>
    /// Disconnect multiple UserNotifications records from User
    /// </summary>
    public Task DisconnectUserNotifications(
        UserWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    );

    /// <summary>
    /// Find multiple UserNotifications records for User
    /// </summary>
    public Task<List<UserNotification>> FindUserNotifications(
        UserWhereUniqueInput uniqueId,
        UserNotificationFindManyArgs UserNotificationFindManyArgs
    );

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple UserNotifications records for User
    /// </summary>
    public Task UpdateUserNotifications(
        UserWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    );
}
