using Microsoft.EntityFrameworkCore;
using NotificationService.APIs;
using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;
using NotificationService.APIs.Errors;
using NotificationService.APIs.Extensions;
using NotificationService.Infrastructure;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs;

public abstract class UserNotificationsServiceBase : IUserNotificationsService
{
    protected readonly NotificationServiceDbContext _context;

    public UserNotificationsServiceBase(NotificationServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one UserNotification
    /// </summary>
    public async Task<UserNotificationDto> CreateUserNotification(
        UserNotificationCreateInput createDto
    )
    {
        var userNotification = new UserNotification
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            userNotification.Id = createDto.Id;
        }
        if (createDto.Notification != null)
        {
            userNotification.Notification = await _context
                .Notifications.Where(notification => createDto.Notification.Id == notification.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.User != null)
        {
            userNotification.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.UserNotifications.Add(userNotification);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserNotification>(userNotification.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one UserNotification
    /// </summary>
    public async Task DeleteUserNotification(UserNotificationIdDto idDto)
    {
        var userNotification = await _context.UserNotifications.FindAsync(idDto.Id);
        if (userNotification == null)
        {
            throw new NotFoundException();
        }

        _context.UserNotifications.Remove(userNotification);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many UserNotifications
    /// </summary>
    public async Task<List<UserNotificationDto>> UserNotifications(
        UserNotificationFindMany findManyArgs
    )
    {
        var userNotifications = await _context
            .UserNotifications.Include(x => x.Notification)
            .Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return userNotifications.ConvertAll(userNotification => userNotification.ToDto());
    }

    /// <summary>
    /// Get one UserNotification
    /// </summary>
    public async Task<UserNotificationDto> UserNotification(UserNotificationIdDto idDto)
    {
        var userNotifications = await this.UserNotifications(
            new UserNotificationFindMany
            {
                Where = new UserNotificationWhereInput { Id = idDto.Id }
            }
        );
        var userNotification = userNotifications.FirstOrDefault();
        if (userNotification == null)
        {
            throw new NotFoundException();
        }

        return userNotification;
    }

    /// <summary>
    /// Update one UserNotification
    /// </summary>
    public async Task UpdateUserNotification(
        UserNotificationIdDto idDto,
        UserNotificationUpdateInput updateDto
    )
    {
        var userNotification = updateDto.ToModel(idDto);

        _context.Entry(userNotification).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.UserNotifications.Any(e => e.Id == userNotification.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Notification record for UserNotification
    /// </summary>
    public async Task<NotificationDto> GetNotification(UserNotificationIdDto idDto)
    {
        var userNotification = await _context
            .UserNotifications.Where(userNotification => userNotification.Id == idDto.Id)
            .Include(userNotification => userNotification.Notification)
            .FirstOrDefaultAsync();
        if (userNotification == null)
        {
            throw new NotFoundException();
        }
        return userNotification.Notification.ToDto();
    }

    /// <summary>
    /// Get a User record for UserNotification
    /// </summary>
    public async Task<UserDto> GetUser(UserNotificationIdDto idDto)
    {
        var userNotification = await _context
            .UserNotifications.Where(userNotification => userNotification.Id == idDto.Id)
            .Include(userNotification => userNotification.User)
            .FirstOrDefaultAsync();
        if (userNotification == null)
        {
            throw new NotFoundException();
        }
        return userNotification.User.ToDto();
    }

    /// <summary>
    /// Meta data about UserNotification records
    /// </summary>
    public async Task<MetadataDto> UserNotificationsMeta(UserNotificationFindMany findManyArgs)
    {
        var count = await _context.UserNotifications.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }
}
