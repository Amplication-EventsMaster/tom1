namespace NotificationService.APIs.Dtos;

public class NotificationTypeUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<string>? Notifications { get; set; }
}
