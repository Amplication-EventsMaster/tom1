using NotificationService.APIs.Dtos;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs.Extensions;

public static class NotificationTypesExtensions
{
    public static NotificationTypeDto ToDto(this NotificationType model)
    {
        return new NotificationTypeDto
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Name = model.Name,
            Notifications = model
                .Notifications?.Select(x => new NotificationIdDto { Id = x.Id })
                .ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static NotificationType ToModel(
        this NotificationTypeUpdateInput updateDto,
        NotificationTypeIdDto idDto
    )
    {
        var notificationType = new NotificationType
        {
            Id = idDto.Id,
            Description = updateDto.Description,
            Name = updateDto.Name
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
