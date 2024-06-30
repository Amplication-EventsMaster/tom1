using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;

namespace NotificationService.APIs;

public interface INotificationTypesService
{
    /// <summary>
    /// Create one NotificationType
    /// </summary>
    public Task<NotificationTypeDto> CreateNotificationType(
        NotificationTypeCreateInput notificationtypeDto
    );

    /// <summary>
    /// Delete one NotificationType
    /// </summary>
    public Task DeleteNotificationType(NotificationTypeIdDto idDto);

    /// <summary>
    /// Find many NotificationTypes
    /// </summary>
    public Task<List<NotificationTypeDto>> NotificationTypes(NotificationTypeFindMany findManyArgs);

    /// <summary>
    /// Get one NotificationType
    /// </summary>
    public Task<NotificationTypeDto> NotificationType(NotificationTypeIdDto idDto);

    /// <summary>
    /// Connect multiple Notifications records to NotificationType
    /// </summary>
    public Task ConnectNotifications(
        NotificationTypeIdDto idDto,
        NotificationIdDto[] notificationsId
    );

    /// <summary>
    /// Disconnect multiple Notifications records from NotificationType
    /// </summary>
    public Task DisconnectNotifications(
        NotificationTypeIdDto idDto,
        NotificationIdDto[] notificationsId
    );

    /// <summary>
    /// Find multiple Notifications records for NotificationType
    /// </summary>
    public Task<List<NotificationDto>> FindNotifications(
        NotificationTypeIdDto idDto,
        NotificationFindMany NotificationFindMany
    );

    /// <summary>
    /// Meta data about NotificationType records
    /// </summary>
    public Task<MetadataDto> NotificationTypesMeta(NotificationTypeFindMany findManyArgs);

    /// <summary>
    /// Update multiple Notifications records for NotificationType
    /// </summary>
    public Task UpdateNotifications(
        NotificationTypeIdDto idDto,
        NotificationIdDto[] notificationsId
    );

    /// <summary>
    /// Update one NotificationType
    /// </summary>
    public Task UpdateNotificationType(
        NotificationTypeIdDto idDto,
        NotificationTypeUpdateInput updateDto
    );
}
