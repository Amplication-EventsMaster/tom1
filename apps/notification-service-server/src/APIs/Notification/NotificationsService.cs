using NotificationService.Infrastructure;

namespace NotificationService.APIs;

public class NotificationsService : NotificationsServiceBase
{
    public NotificationsService(NotificationServiceDbContext context)
        : base(context) { }
}
