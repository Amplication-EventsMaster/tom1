using Microsoft.AspNetCore.Mvc;

namespace NotificationService.APIs;

[ApiController()]
public class NotificationsController : NotificationsControllerBase
{
    public NotificationsController(INotificationsService service)
        : base(service) { }
}
