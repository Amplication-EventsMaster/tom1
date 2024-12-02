using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Infrastructure.Models;

[Table("NotificationTypes")]
public class NotificationTypeDbModel
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public List<NotificationDbModel>? Notifications { get; set; } = new List<NotificationDbModel>();
}
