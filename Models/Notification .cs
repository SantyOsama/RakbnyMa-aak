using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.Models
{
    public class Notification : BaseEntity
    {
        [Required(ErrorMessage = "User ID is required.")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Notification message is required.")]
        [MaxLength(500, ErrorMessage = "Notification message must not exceed 500 characters.")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Is Read")]
        public bool IsRead { get; set; } = false;
        public NotificationType Type { get; set; } = NotificationType.Custom;


        public string? RelatedEntityId { get; set; }


        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public virtual ApplicationUser User { get; set; }
    }
}
