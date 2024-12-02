using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;

namespace NotificationService.APIs;

public interface INotificationsService
{
    /// <summary>
    /// Create one Notification
    /// </summary>
    public Task<Notification> CreateNotification(NotificationCreateInput notification);

    /// <summary>
    /// Delete one Notification
    /// </summary>
    public Task DeleteNotification(NotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Notifications
    /// </summary>
    public Task<List<Notification>> Notifications(NotificationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Notification
    /// </summary>
    public Task<Notification> Notification(NotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple UserNotifications records to Notification
    /// </summary>
    public Task ConnectUserNotifications(
        NotificationWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    );

    /// <summary>
    /// Disconnect multiple UserNotifications records from Notification
    /// </summary>
    public Task DisconnectUserNotifications(
        NotificationWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    );

    /// <summary>
    /// Find multiple UserNotifications records for Notification
    /// </summary>
    public Task<List<UserNotification>> FindUserNotifications(
        NotificationWhereUniqueInput uniqueId,
        UserNotificationFindManyArgs UserNotificationFindManyArgs
    );

    /// <summary>
    /// Get a NotificationType record for Notification
    /// </summary>
    public Task<NotificationType> GetNotificationType(NotificationWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Notification records
    /// </summary>
    public Task<MetadataDto> NotificationsMeta(NotificationFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple UserNotifications records for Notification
    /// </summary>
    public Task UpdateUserNotifications(
        NotificationWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    );

    /// <summary>
    /// Update one Notification
    /// </summary>
    public Task UpdateNotification(
        NotificationWhereUniqueInput uniqueId,
        NotificationUpdateInput updateDto
    );
}
