using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;

namespace NotificationService.APIs;

public interface IUserNotificationsService
{
    /// <summary>
    /// Create one UserNotification
    /// </summary>
    public Task<UserNotification> CreateUserNotification(
        UserNotificationCreateInput usernotification
    );

    /// <summary>
    /// Delete one UserNotification
    /// </summary>
    public Task DeleteUserNotification(UserNotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many UserNotifications
    /// </summary>
    public Task<List<UserNotification>> UserNotifications(
        UserNotificationFindManyArgs findManyArgs
    );

    /// <summary>
    /// Get one UserNotification
    /// </summary>
    public Task<UserNotification> UserNotification(UserNotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one UserNotification
    /// </summary>
    public Task UpdateUserNotification(
        UserNotificationWhereUniqueInput uniqueId,
        UserNotificationUpdateInput updateDto
    );

    /// <summary>
    /// Get a Notification record for UserNotification
    /// </summary>
    public Task<Notification> GetNotification(UserNotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a User record for UserNotification
    /// </summary>
    public Task<User> GetUser(UserNotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about UserNotification records
    /// </summary>
    public Task<MetadataDto> UserNotificationsMeta(UserNotificationFindManyArgs findManyArgs);
}
