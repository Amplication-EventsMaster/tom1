using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Infrastructure.Models;

[Table("UserNotifications")]
public class UserNotification
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? NotificationId { get; set; }

    [ForeignKey(nameof(NotificationId))]
    public Notification? Notification { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; } = null;
}
