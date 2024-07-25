using NotificationService.APIs.Dtos;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs.Extensions;

public static class NotificationsExtensions
{
    public static Notification ToDto(this NotificationDbModel model)
    {
        return new Notification
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Title = model.Title,
            Message = model.Message,
            Read = model.Read,
            UserNotifications = model.UserNotifications?.Select(x => x.Id).ToList(),
            NotificationType = model.NotificationTypeId,
        };
    }

    public static NotificationDbModel ToModel(
        this NotificationUpdateInput updateDto,
        NotificationWhereUniqueInput uniqueId
    )
    {
        var notification = new NotificationDbModel
        {
            Id = uniqueId.Id,
            Title = updateDto.Title,
            Message = updateDto.Message,
            Read = updateDto.Read
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
