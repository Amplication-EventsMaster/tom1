using NotificationService.Infrastructure;

namespace NotificationService.APIs;

public class UserNotificationsService : UserNotificationsServiceBase
{
    public UserNotificationsService(NotificationServiceDbContext context)
        : base(context) { }
}
