using NotificationService.APIs.Dtos;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs.Extensions;

public static class UsersExtensions
{
    public static User ToDto(this UserDbModel model)
    {
        return new User
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Username = model.Username,
            Email = model.Email,
            Password = model.Password,
            Roles = model.Roles,
            UserNotifications = model.UserNotifications?.Select(x => x.Id).ToList(),
        };
    }

    public static UserDbModel ToModel(this UserUpdateInput updateDto, UserWhereUniqueInput uniqueId)
    {
        var user = new UserDbModel
        {
            Id = uniqueId.Id,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            Email = updateDto.Email
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            user.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            user.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.Username != null)
        {
            user.Username = updateDto.Username;
        }
        if (updateDto.Password != null)
        {
            user.Password = updateDto.Password;
        }
        if (updateDto.Roles != null)
        {
            user.Roles = updateDto.Roles;
        }

        return user;
    }
}
