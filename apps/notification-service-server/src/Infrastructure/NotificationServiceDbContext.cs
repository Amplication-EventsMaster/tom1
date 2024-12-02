using Microsoft.EntityFrameworkCore;
using NotificationService.Infrastructure.Models;

namespace NotificationService.Infrastructure;

public class NotificationServiceDbContext : DbContext
{
    public NotificationServiceDbContext(DbContextOptions<NotificationServiceDbContext> options)
        : base(options) { }

    public DbSet<NotificationDbModel> Notifications { get; set; }

    public DbSet<NotificationTypeDbModel> NotificationTypes { get; set; }

    public DbSet<UserNotificationDbModel> UserNotifications { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
