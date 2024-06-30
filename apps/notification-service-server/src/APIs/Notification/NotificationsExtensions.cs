using NotificationService.APIs.Dtos;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs.Extensions;

public static class NotificationsExtensions
{
    public static NotificationDto ToDto(this Notification model)
    {
        return new NotificationDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Message = model.Message,
            NotificationType = new NotificationTypeIdDto { Id = model.NotificationTypeId },
            Read = model.Read,
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,
            UserNotifications = model
                .UserNotifications?.Select(x => new UserNotificationIdDto { Id = x.Id })
                .ToList(),
        };
    }

    public static Notification ToModel(
        this NotificationUpdateInput updateDto,
        NotificationIdDto idDto
    )
    {
        var notification = new Notification
        {
            Id = idDto.Id,
            Message = updateDto.Message,
            Read = updateDto.Read,
            Title = updateDto.Title
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            notification.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            notification.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return notification;
    }
}
