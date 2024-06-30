using Microsoft.EntityFrameworkCore;
using NotificationService.Infrastructure.Models;

namespace NotificationService.Infrastructure;

public class NotificationServiceDbContext : DbContext
{
    public NotificationServiceDbContext(DbContextOptions<NotificationServiceDbContext> options)
        : base(options) { }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<NotificationType> NotificationTypes { get; set; }

    public DbSet<UserNotification> UserNotifications { get; set; }

    public DbSet<User> Users { get; set; }
}
