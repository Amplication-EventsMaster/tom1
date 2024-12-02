using Microsoft.AspNetCore.Mvc;

namespace NotificationService.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
