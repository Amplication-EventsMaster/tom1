using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;

namespace NotificationService.APIs;

public interface IUserNotificationsService
{
    /// <summary>
    /// Create one UserNotification
    /// </summary>
    public Task<UserNotificationDto> CreateUserNotification(
        UserNotificationCreateInput usernotificationDto
    );

    /// <summary>
    /// Delete one UserNotification
    /// </summary>
    public Task DeleteUserNotification(UserNotificationIdDto idDto);

    /// <summary>
    /// Find many UserNotifications
    /// </summary>
    public Task<List<UserNotificationDto>> UserNotifications(UserNotificationFindMany findManyArgs);

    /// <summary>
    /// Get one UserNotification
    /// </summary>
    public Task<UserNotificationDto> UserNotification(UserNotificationIdDto idDto);

    /// <summary>
    /// Update one UserNotification
    /// </summary>
    public Task UpdateUserNotification(
        UserNotificationIdDto idDto,
        UserNotificationUpdateInput updateDto
    );

    /// <summary>
    /// Get a Notification record for UserNotification
    /// </summary>
    public Task<NotificationDto> GetNotification(UserNotificationIdDto idDto);

    /// <summary>
    /// Get a User record for UserNotification
    /// </summary>
    public Task<UserDto> GetUser(UserNotificationIdDto idDto);

    /// <summary>
    /// Meta data about UserNotification records
    /// </summary>
    public Task<MetadataDto> UserNotificationsMeta(UserNotificationFindMany findManyArgs);
}
