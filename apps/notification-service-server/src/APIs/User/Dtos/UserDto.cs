namespace NotificationService.APIs.Dtos;

public class UserDto : UserIdDto
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Password { get; set; }

    public string Roles { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Username { get; set; }

    public List<UserNotificationIdDto>? UserNotifications { get; set; }
}
