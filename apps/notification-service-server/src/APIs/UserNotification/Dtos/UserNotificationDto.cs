namespace NotificationService.APIs.Dtos;

public class UserNotificationDto : UserNotificationIdDto
{
    public DateTime CreatedAt { get; set; }

    public NotificationIdDto? Notification { get; set; }

    public DateTime UpdatedAt { get; set; }

    public UserIdDto? User { get; set; }
}
