namespace NotificationService.APIs.Dtos;

public class NotificationUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Message { get; set; }

    public NotificationTypeIdDto? NotificationType { get; set; }

    public bool? Read { get; set; }

    public string? Title { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<UserNotificationIdDto>? UserNotifications { get; set; }
}
