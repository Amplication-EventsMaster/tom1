namespace NotificationService.APIs.Dtos;

public class NotificationWhereInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public bool? Read { get; set; }

    public List<string>? UserNotifications { get; set; }

    public string? NotificationType { get; set; }
}
