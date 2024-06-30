using NotificationService.APIs;

namespace NotificationService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationsService, NotificationsService>();
        services.AddScoped<INotificationTypesService, NotificationTypesService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IUserNotificationsService, UserNotificationsService>();
    }
}
