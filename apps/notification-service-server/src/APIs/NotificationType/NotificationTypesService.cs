using NotificationService.Infrastructure;

namespace NotificationService.APIs;

public class NotificationTypesService : NotificationTypesServiceBase
{
    public NotificationTypesService(NotificationServiceDbContext context)
        : base(context) { }
}
