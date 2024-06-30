namespace NotificationService.APIs.Dtos;

public class UserNotificationWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public NotificationIdDto? Notification { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public UserIdDto? User { get; set; }
}
