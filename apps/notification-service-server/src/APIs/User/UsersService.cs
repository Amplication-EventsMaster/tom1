using NotificationService.Infrastructure;

namespace NotificationService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(NotificationServiceDbContext context)
        : base(context) { }
}
