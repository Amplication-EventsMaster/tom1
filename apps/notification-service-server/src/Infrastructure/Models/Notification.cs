using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Infrastructure.Models;

[Table("Notifications")]
public class NotificationDbModel
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Title { get; set; }

    [StringLength(1000)]
    public string? Message { get; set; }

    public bool? Read { get; set; }

    public List<UserNotificationDbModel>? UserNotifications { get; set; } =
        new List<UserNotificationDbModel>();

    public string? NotificationTypeId { get; set; }

    [ForeignKey(nameof(NotificationTypeId))]
    public NotificationTypeDbModel? NotificationType { get; set; } = null;
}
