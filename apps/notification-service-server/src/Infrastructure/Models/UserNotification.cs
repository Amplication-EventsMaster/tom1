using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Infrastructure.Models;

[Table("UserNotifications")]
public class UserNotificationDbModel
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserDbModel? User { get; set; } = null;

    public string? NotificationId { get; set; }

    [ForeignKey(nameof(NotificationId))]
    public NotificationDbModel? Notification { get; set; } = null;
}
