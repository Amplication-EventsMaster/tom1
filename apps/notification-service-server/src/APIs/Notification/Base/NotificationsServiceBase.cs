using Microsoft.EntityFrameworkCore;
using NotificationService.APIs;
using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;
using NotificationService.APIs.Errors;
using NotificationService.APIs.Extensions;
using NotificationService.Infrastructure;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs;

public abstract class NotificationsServiceBase : INotificationsService
{
    protected readonly NotificationServiceDbContext _context;

    public NotificationsServiceBase(NotificationServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Notification
    /// </summary>
    public async Task<NotificationDto> CreateNotification(NotificationCreateInput createDto)
    {
        var notification = new Notification
        {
            CreatedAt = createDto.CreatedAt,
            Message = createDto.Message,
            Read = createDto.Read,
            Title = createDto.Title,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            notification.Id = createDto.Id;
        }
        if (createDto.NotificationType != null)
        {
            notification.NotificationType = await _context
                .NotificationTypes.Where(notificationType =>
                    createDto.NotificationType.Id == notificationType.Id
                )
                .FirstOrDefaultAsync();
        }

        if (createDto.UserNotifications != null)
        {
            notification.UserNotifications = await _context
                .UserNotifications.Where(userNotification =>
                    createDto.UserNotifications.Select(t => t.Id).Contains(userNotification.Id)
                )
                .ToListAsync();
        }

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Notification>(notification.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Notification
    /// </summary>
    public async Task DeleteNotification(NotificationIdDto idDto)
    {
        var notification = await _context.Notifications.FindAsync(idDto.Id);
        if (notification == null)
        {
            throw new NotFoundException();
        }

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Notifications
    /// </summary>
    public async Task<List<NotificationDto>> Notifications(NotificationFindMany findManyArgs)
    {
        var notifications = await _context
            .Notifications.Include(x => x.NotificationType)
            .Include(x => x.UserNotifications)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return notifications.ConvertAll(notification => notification.ToDto());
    }

    /// <summary>
    /// Get one Notification
    /// </summary>
    public async Task<NotificationDto> Notification(NotificationIdDto idDto)
    {
        var notifications = await this.Notifications(
            new NotificationFindMany { Where = new NotificationWhereInput { Id = idDto.Id } }
        );
        var notification = notifications.FirstOrDefault();
        if (notification == null)
        {
            throw new NotFoundException();
        }

        return notification;
    }

    /// <summary>
    /// Connect multiple UserNotifications records to Notification
    /// </summary>
    public async Task ConnectUserNotifications(
        NotificationIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    )
    {
        var notification = await _context
            .Notifications.Include(x => x.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (notification == null)
        {
            throw new NotFoundException();
        }

        var userNotifications = await _context
            .UserNotifications.Where(t => userNotificationsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (userNotifications.Count == 0)
        {
            throw new NotFoundException();
        }

        var userNotificationsToConnect = userNotifications.Except(notification.UserNotifications);

        foreach (var userNotification in userNotificationsToConnect)
        {
            notification.UserNotifications.Add(userNotification);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple UserNotifications records from Notification
    /// </summary>
    public async Task DisconnectUserNotifications(
        NotificationIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    )
    {
        var notification = await _context
            .Notifications.Include(x => x.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (notification == null)
        {
            throw new NotFoundException();
        }

        var userNotifications = await _context
            .UserNotifications.Where(t => userNotificationsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var userNotification in userNotifications)
        {
            notification.UserNotifications?.Remove(userNotification);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple UserNotifications records for Notification
    /// </summary>
    public async Task<List<UserNotificationDto>> FindUserNotifications(
        NotificationIdDto idDto,
        UserNotificationFindMany notificationFindMany
    )
    {
        var userNotifications = await _context
            .UserNotifications.Where(m => m.NotificationId == idDto.Id)
            .ApplyWhere(notificationFindMany.Where)
            .ApplySkip(notificationFindMany.Skip)
            .ApplyTake(notificationFindMany.Take)
            .ApplyOrderBy(notificationFindMany.SortBy)
            .ToListAsync();

        return userNotifications.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Get a NotificationType record for Notification
    /// </summary>
    public async Task<NotificationTypeDto> GetNotificationType(NotificationIdDto idDto)
    {
        var notification = await _context
            .Notifications.Where(notification => notification.Id == idDto.Id)
            .Include(notification => notification.NotificationType)
            .FirstOrDefaultAsync();
        if (notification == null)
        {
            throw new NotFoundException();
        }
        return notification.NotificationType.ToDto();
    }

    /// <summary>
    /// Meta data about Notification records
    /// </summary>
    public async Task<MetadataDto> NotificationsMeta(NotificationFindMany findManyArgs)
    {
        var count = await _context.Notifications.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple UserNotifications records for Notification
    /// </summary>
    public async Task UpdateUserNotifications(
        NotificationIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    )
    {
        var notification = await _context
            .Notifications.Include(t => t.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (notification == null)
        {
            throw new NotFoundException();
        }

        var userNotifications = await _context
            .UserNotifications.Where(a => userNotificationsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (userNotifications.Count == 0)
        {
            throw new NotFoundException();
        }

        notification.UserNotifications = userNotifications;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Notification
    /// </summary>
    public async Task UpdateNotification(NotificationIdDto idDto, NotificationUpdateInput updateDto)
    {
        var notification = updateDto.ToModel(idDto);

        if (updateDto.UserNotifications != null)
        {
            notification.UserNotifications = await _context
                .UserNotifications.Where(userNotification =>
                    updateDto.UserNotifications.Select(t => t.Id).Contains(userNotification.Id)
                )
                .ToListAsync();
        }

        _context.Entry(notification).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Notifications.Any(e => e.Id == notification.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
