using Microsoft.EntityFrameworkCore;
using NotificationService.APIs;
using NotificationService.APIs.Common;
using NotificationService.APIs.Dtos;
using NotificationService.APIs.Errors;
using NotificationService.APIs.Extensions;
using NotificationService.Infrastructure;
using NotificationService.Infrastructure.Models;

namespace NotificationService.APIs;

public abstract class UsersServiceBase : IUsersService
{
    protected readonly NotificationServiceDbContext _context;

    public UsersServiceBase(NotificationServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    public async Task<User> CreateUser(UserCreateInput createDto)
    {
        var user = new UserDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Username = createDto.Username,
            Email = createDto.Email,
            Password = createDto.Password,
            Roles = createDto.Roles
        };

        if (createDto.Id != null)
        {
            user.Id = createDto.Id;
        }
        if (createDto.UserNotifications != null)
        {
            user.UserNotifications = await _context
                .UserNotifications.Where(userNotification =>
                    createDto.UserNotifications.Select(t => t.Id).Contains(userNotification.Id)
                )
                .ToListAsync();
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserDbModel>(user.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    public async Task DeleteUser(UserWhereUniqueInput uniqueId)
    {
        var user = await _context.Users.FindAsync(uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    public async Task<List<User>> Users(UserFindManyArgs findManyArgs)
    {
        var users = await _context
            .Users.Include(x => x.UserNotifications)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return users.ConvertAll(user => user.ToDto());
    }

    /// <summary>
    /// Get one User
    /// </summary>
    public async Task<User> User(UserWhereUniqueInput uniqueId)
    {
        var users = await this.Users(
            new UserFindManyArgs { Where = new UserWhereInput { Id = uniqueId.Id } }
        );
        var user = users.FirstOrDefault();
        if (user == null)
        {
            throw new NotFoundException();
        }

        return user;
    }

    /// <summary>
    /// Update one User
    /// </summary>
    public async Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto)
    {
        var user = updateDto.ToModel(uniqueId);

        if (updateDto.UserNotifications != null)
        {
            user.UserNotifications = await _context
                .UserNotifications.Where(userNotification =>
                    updateDto.UserNotifications.Select(t => t).Contains(userNotification.Id)
                )
                .ToListAsync();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(e => e.Id == user.Id))
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
    /// Connect multiple UserNotifications records to User
    /// </summary>
    public async Task ConnectUserNotifications(
        UserWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    )
    {
        var user = await _context
            .Users.Include(x => x.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
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

        var userNotificationsToConnect = userNotifications.Except(user.UserNotifications);

        foreach (var userNotification in userNotificationsToConnect)
        {
            user.UserNotifications.Add(userNotification);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple UserNotifications records from User
    /// </summary>
    public async Task DisconnectUserNotifications(
        UserWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    )
    {
        var user = await _context
            .Users.Include(x => x.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        var userNotifications = await _context
            .UserNotifications.Where(t => userNotificationsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var userNotification in userNotifications)
        {
            user.UserNotifications?.Remove(userNotification);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple UserNotifications records for User
    /// </summary>
    public async Task<List<UserNotification>> FindUserNotifications(
        UserWhereUniqueInput uniqueId,
        UserNotificationFindManyArgs userFindManyArgs
    )
    {
        var userNotifications = await _context
            .UserNotifications.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return userNotifications.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public async Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs)
    {
        var count = await _context.Users.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple UserNotifications records for User
    /// </summary>
    public async Task UpdateUserNotifications(
        UserWhereUniqueInput uniqueId,
        UserNotificationWhereUniqueInput[] userNotificationsId
    )
    {
        var user = await _context
            .Users.Include(t => t.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
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

        user.UserNotifications = userNotifications;
        await _context.SaveChangesAsync();
    }
}
