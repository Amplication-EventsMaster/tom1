using Microsoft.AspNetCore.Mvc;

namespace NotificationService.APIs;

[ApiController()]
public class NotificationTypesController : NotificationTypesControllerBase
{
    public NotificationTypesController(INotificationTypesService service)
        : base(service) { }
}
