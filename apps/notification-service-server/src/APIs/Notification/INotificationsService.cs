using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;

namespace NotificationService.APIs;

public interface INotificationsService
{
    /// <summary>
    /// Create one Notification
    /// </summary>
    public Task<NotificationDto> CreateNotification(NotificationCreateInput notificationDto);

    /// <summary>
    /// Delete one Notification
    /// </summary>
    public Task DeleteNotification(NotificationIdDto idDto);

    /// <summary>
    /// Find many Notifications
    /// </summary>
    public Task<List<NotificationDto>> Notifications(NotificationFindMany findManyArgs);

    /// <summary>
    /// Get one Notification
    /// </summary>
    public Task<NotificationDto> Notification(NotificationIdDto idDto);

    /// <summary>
    /// Connect multiple UserNotifications records to Notification
    /// </summary>
    public Task ConnectUserNotifications(
        NotificationIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    );

    /// <summary>
    /// Disconnect multiple UserNotifications records from Notification
    /// </summary>
    public Task DisconnectUserNotifications(
        NotificationIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    );

    /// <summary>
    /// Find multiple UserNotifications records for Notification
    /// </summary>
    public Task<List<UserNotificationDto>> FindUserNotifications(
        NotificationIdDto idDto,
        UserNotificationFindMany UserNotificationFindMany
    );

    /// <summary>
    /// Get a NotificationType record for Notification
    /// </summary>
    public Task<NotificationTypeDto> GetNotificationType(NotificationIdDto idDto);

    /// <summary>
    /// Meta data about Notification records
    /// </summary>
    public Task<MetadataDto> NotificationsMeta(NotificationFindMany findManyArgs);

    /// <summary>
    /// Update multiple UserNotifications records for Notification
    /// </summary>
    public Task UpdateUserNotifications(
        NotificationIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    );

    /// <summary>
    /// Update one Notification
    /// </summary>
    public Task UpdateNotification(NotificationIdDto idDto, NotificationUpdateInput updateDto);
}
