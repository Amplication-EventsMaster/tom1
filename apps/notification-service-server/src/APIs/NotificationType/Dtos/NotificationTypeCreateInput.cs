namespace NotificationService.APIs.Dtos;

public class NotificationTypeCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<Notification>? Notifications { get; set; }
}
