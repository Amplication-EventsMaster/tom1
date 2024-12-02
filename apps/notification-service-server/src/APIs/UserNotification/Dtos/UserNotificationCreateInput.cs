namespace NotificationService.APIs.Dtos;

public class UserNotificationCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }

    public Notification? Notification { get; set; }
}
