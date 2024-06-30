using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Infrastructure.Models;

[Table("NotificationTypes")]
public class NotificationType
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<Notification>? Notifications { get; set; } = new List<Notification>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
