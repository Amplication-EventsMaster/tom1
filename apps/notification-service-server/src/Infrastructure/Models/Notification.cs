using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Infrastructure.Models;

[Table("Notifications")]
public class Notification
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Message { get; set; }

    public string? NotificationTypeId { get; set; }

    [ForeignKey(nameof(NotificationTypeId))]
    public NotificationType? NotificationType { get; set; } = null;

    public bool? Read { get; set; }

    [StringLength(1000)]
    public string? Title { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public List<UserNotification>? UserNotifications { get; set; } = new List<UserNotification>();
}
