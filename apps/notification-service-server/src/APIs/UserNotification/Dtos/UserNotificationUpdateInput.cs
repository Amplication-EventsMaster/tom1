namespace NotificationService.APIs.Dtos;

public class UserNotificationUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? User { get; set; }

    public string? Notification { get; set; }
}
