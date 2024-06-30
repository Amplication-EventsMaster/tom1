using NotificationService.APIs.Dtos;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs.Extensions;

public static class UserNotificationsExtensions
{
    public static UserNotificationDto ToDto(this UserNotification model)
    {
        return new UserNotificationDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Notification = new NotificationIdDto { Id = model.NotificationId },
            UpdatedAt = model.UpdatedAt,
            User = new UserIdDto { Id = model.UserId },
        };
    }

    public static UserNotification ToModel(
        this UserNotificationUpdateInput updateDto,
        UserNotificationIdDto idDto
    )
    {
        var userNotification = new UserNotification { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            userNotification.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            userNotification.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return userNotification;
    }
}
