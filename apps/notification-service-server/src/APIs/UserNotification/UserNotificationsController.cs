using Microsoft.AspNetCore.Mvc;

namespace NotificationService.APIs;

[ApiController()]
public class UserNotificationsController : UserNotificationsControllerBase
{
    public UserNotificationsController(IUserNotificationsService service)
        : base(service) { }
}
