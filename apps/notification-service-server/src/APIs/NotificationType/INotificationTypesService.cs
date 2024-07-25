using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;

namespace NotificationService.APIs;

public interface INotificationTypesService
{
    /// <summary>
    /// Create one NotificationType
    /// </summary>
    public Task<NotificationType> CreateNotificationType(
        NotificationTypeCreateInput notificationtype
    );

    /// <summary>
    /// Delete one NotificationType
    /// </summary>
    public Task DeleteNotificationType(NotificationTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many NotificationTypes
    /// </summary>
    public Task<List<NotificationType>> NotificationTypes(
        NotificationTypeFindManyArgs findManyArgs
    );

    /// <summary>
    /// Get one NotificationType
    /// </summary>
    public Task<NotificationType> NotificationType(NotificationTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Notifications records to NotificationType
    /// </summary>
    public Task ConnectNotifications(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Disconnect multiple Notifications records from NotificationType
    /// </summary>
    public Task DisconnectNotifications(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Find multiple Notifications records for NotificationType
    /// </summary>
    public Task<List<Notification>> FindNotifications(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationFindManyArgs NotificationFindManyArgs
    );

    /// <summary>
    /// Meta data about NotificationType records
    /// </summary>
    public Task<MetadataDto> NotificationTypesMeta(NotificationTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple Notifications records for NotificationType
    /// </summary>
    public Task UpdateNotifications(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    );

    /// <summary>
    /// Update one NotificationType
    /// </summary>
    public Task UpdateNotificationType(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationTypeUpdateInput updateDto
    );
}
