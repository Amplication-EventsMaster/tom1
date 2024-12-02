using NotificationService.APIs.Dtos;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs.Extensions;

public static class NotificationTypesExtensions
{
    public static NotificationType ToDto(this NotificationTypeDbModel model)
    {
        return new NotificationType
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            Description = model.Description,
            Notifications = model.Notifications?.Select(x => x.Id).ToList(),
        };
    }

    public static NotificationTypeDbModel ToModel(
        this NotificationTypeUpdateInput updateDto,
        NotificationTypeWhereUniqueInput uniqueId
    )
    {
        var notificationType = new NotificationTypeDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Description = updateDto.Description
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            notificationType.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            notificationType.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return notificationType;
    }
}
