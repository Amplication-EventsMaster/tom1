namespace NotificationService.APIs.Dtos;

public class NotificationCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public bool? Read { get; set; }

    public List<UserNotification>? UserNotifications { get; set; }

    public NotificationType? NotificationType { get; set; }
}
