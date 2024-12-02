using Microsoft.EntityFrameworkCore;
using NotificationService.APIs;
using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;
using NotificationService.APIs.Errors;
using NotificationService.APIs.Extensions;
using NotificationService.Infrastructure;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs;

public abstract class NotificationTypesServiceBase : INotificationTypesService
{
    protected readonly NotificationServiceDbContext _context;

    public NotificationTypesServiceBase(NotificationServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one NotificationType
    /// </summary>
    public async Task<NotificationType> CreateNotificationType(
        NotificationTypeCreateInput createDto
    )
    {
        var notificationType = new NotificationTypeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Name = createDto.Name,
            Description = createDto.Description
        };

        if (createDto.Id != null)
        {
            notificationType.Id = createDto.Id;
        }
        if (createDto.Notifications != null)
        {
            notificationType.Notifications = await _context
                .Notifications.Where(notification =>
                    createDto.Notifications.Select(t => t.Id).Contains(notification.Id)
                )
                .ToListAsync();
        }

        _context.NotificationTypes.Add(notificationType);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<NotificationTypeDbModel>(notificationType.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one NotificationType
    /// </summary>
    public async Task DeleteNotificationType(NotificationTypeWhereUniqueInput uniqueId)
    {
        var notificationType = await _context.NotificationTypes.FindAsync(uniqueId.Id);
        if (notificationType == null)
        {
            throw new NotFoundException();
        }

        _context.NotificationTypes.Remove(notificationType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many NotificationTypes
    /// </summary>
    public async Task<List<NotificationType>> NotificationTypes(
        NotificationTypeFindManyArgs findManyArgs
    )
    {
        var notificationTypes = await _context
            .NotificationTypes.Include(x => x.Notifications)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return notificationTypes.ConvertAll(notificationType => notificationType.ToDto());
    }

    /// <summary>
    /// Get one NotificationType
    /// </summary>
    public async Task<NotificationType> NotificationType(NotificationTypeWhereUniqueInput uniqueId)
    {
        var notificationTypes = await this.NotificationTypes(
            new NotificationTypeFindManyArgs
            {
                Where = new NotificationTypeWhereInput { Id = uniqueId.Id }
            }
        );
        var notificationType = notificationTypes.FirstOrDefault();
        if (notificationType == null)
        {
            throw new NotFoundException();
        }

        return notificationType;
    }

    /// <summary>
    /// Connect multiple Notifications records to NotificationType
    /// </summary>
    public async Task ConnectNotifications(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    )
    {
        var notificationType = await _context
            .NotificationTypes.Include(x => x.Notifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (notificationType == null)
        {
            throw new NotFoundException();
        }

        var notifications = await _context
            .Notifications.Where(t => notificationsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (notifications.Count == 0)
        {
            throw new NotFoundException();
        }

        var notificationsToConnect = notifications.Except(notificationType.Notifications);

        foreach (var notification in notificationsToConnect)
        {
            notificationType.Notifications.Add(notification);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Notifications records from NotificationType
    /// </summary>
    public async Task DisconnectNotifications(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    )
    {
        var notificationType = await _context
            .NotificationTypes.Include(x => x.Notifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (notificationType == null)
        {
            throw new NotFoundException();
        }

        var notifications = await _context
            .Notifications.Where(t => notificationsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var notification in notifications)
        {
            notificationType.Notifications?.Remove(notification);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Notifications records for NotificationType
    /// </summary>
    public async Task<List<Notification>> FindNotifications(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationFindManyArgs notificationTypeFindManyArgs
    )
    {
        var notifications = await _context
            .Notifications.Where(m => m.NotificationTypeId == uniqueId.Id)
            .ApplyWhere(notificationTypeFindManyArgs.Where)
            .ApplySkip(notificationTypeFindManyArgs.Skip)
            .ApplyTake(notificationTypeFindManyArgs.Take)
            .ApplyOrderBy(notificationTypeFindManyArgs.SortBy)
            .ToListAsync();

        return notifications.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about NotificationType records
    /// </summary>
    public async Task<MetadataDto> NotificationTypesMeta(NotificationTypeFindManyArgs findManyArgs)
    {
        var count = await _context.NotificationTypes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple Notifications records for NotificationType
    /// </summary>
    public async Task UpdateNotifications(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationWhereUniqueInput[] notificationsId
    )
    {
        var notificationType = await _context
            .NotificationTypes.Include(t => t.Notifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (notificationType == null)
        {
            throw new NotFoundException();
        }

        var notifications = await _context
            .Notifications.Where(a => notificationsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (notifications.Count == 0)
        {
            throw new NotFoundException();
        }

        notificationType.Notifications = notifications;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one NotificationType
    /// </summary>
    public async Task UpdateNotificationType(
        NotificationTypeWhereUniqueInput uniqueId,
        NotificationTypeUpdateInput updateDto
    )
    {
        var notificationType = updateDto.ToModel(uniqueId);

        if (updateDto.Notifications != null)
        {
            notificationType.Notifications = await _context
                .Notifications.Where(notification =>
                    updateDto.Notifications.Select(t => t).Contains(notification.Id)
                )
                .ToListAsync();
        }

        _context.Entry(notificationType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.NotificationTypes.Any(e => e.Id == notificationType.Id))
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
