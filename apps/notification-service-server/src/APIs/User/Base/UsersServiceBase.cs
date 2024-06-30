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
    public async Task<UserDto> CreateUser(UserCreateInput createDto)
    {
        var user = new User
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Password = createDto.Password,
            Roles = createDto.Roles,
            UpdatedAt = createDto.UpdatedAt,
            Username = createDto.Username
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

        var result = await _context.FindAsync<User>(user.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    public async Task DeleteUser(UserIdDto idDto)
    {
        var user = await _context.Users.FindAsync(idDto.Id);
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
    public async Task<List<UserDto>> Users(UserFindMany findManyArgs)
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
    public async Task<UserDto> User(UserIdDto idDto)
    {
        var users = await this.Users(
            new UserFindMany { Where = new UserWhereInput { Id = idDto.Id } }
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
    public async Task UpdateUser(UserIdDto idDto, UserUpdateInput updateDto)
    {
        var user = updateDto.ToModel(idDto);

        if (updateDto.UserNotifications != null)
        {
            user.UserNotifications = await _context
                .UserNotifications.Where(userNotification =>
                    updateDto.UserNotifications.Select(t => t.Id).Contains(userNotification.Id)
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
        UserIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    )
    {
        var user = await _context
            .Users.Include(x => x.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
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
        UserIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    )
    {
        var user = await _context
            .Users.Include(x => x.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
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
    public async Task<List<UserNotificationDto>> FindUserNotifications(
        UserIdDto idDto,
        UserNotificationFindMany userFindMany
    )
    {
        var userNotifications = await _context
            .UserNotifications.Where(m => m.UserId == idDto.Id)
            .ApplyWhere(userFindMany.Where)
            .ApplySkip(userFindMany.Skip)
            .ApplyTake(userFindMany.Take)
            .ApplyOrderBy(userFindMany.SortBy)
            .ToListAsync();

        return userNotifications.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public async Task<MetadataDto> UsersMeta(UserFindMany findManyArgs)
    {
        var count = await _context.Users.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple UserNotifications records for User
    /// </summary>
    public async Task UpdateUserNotifications(
        UserIdDto idDto,
        UserNotificationIdDto[] userNotificationsId
    )
    {
        var user = await _context
            .Users.Include(t => t.UserNotifications)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
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
