using NotificationService.APIs.Dtos;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs.Extensions;

public static class UserNotificationsExtensions
{
    public static UserNotification ToDto(this UserNotificationDbModel model)
    {
        return new UserNotification
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
            Notification = model.NotificationId,
        };
    }

    public static UserNotificationDbModel ToModel(
        this UserNotificationUpdateInput updateDto,
        UserNotificationWhereUniqueInput uniqueId
    )
    {
        var userNotification = new UserNotificationDbModel { Id = uniqueId.Id };

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
