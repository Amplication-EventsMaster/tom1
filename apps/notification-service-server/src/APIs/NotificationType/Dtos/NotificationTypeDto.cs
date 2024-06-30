namespace NotificationService.APIs.Dtos;

public class NotificationTypeDto : NotificationTypeIdDto
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public List<NotificationIdDto>? Notifications { get; set; }

    public DateTime UpdatedAt { get; set; }
}
