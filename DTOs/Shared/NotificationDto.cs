using System.ComponentModel.DataAnnotations;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.DTOs.Shared
{
    public class NotificationDto
    {
        [Required(ErrorMessage = "Message is required.")]
        [MaxLength(500, ErrorMessage = "Message must not exceed 500 characters.")]
        [Display(Name = "Notification Message")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Sender ID is required.")]
        [Display(Name = "Sender ID")]
        public string SenderId { get; set; }

        [Required(ErrorMessage = "Sender full name is required.")]
        [MaxLength(100, ErrorMessage = "Sender full name must not exceed 100 characters.")]
        [Display(Name = "Sender Full Name")]
        public string SenderFullName { get; set; }

        [Required(ErrorMessage = "Sender picture URL is required.")]
        [Url(ErrorMessage = "Invalid URL format for sender picture.")]
        [Display(Name = "Sender Picture URL")]
        public string SenderPicture { get; set; }

        [Display(Name = "Notification Type")]
        public NotificationType Type { get; set; } 

        [Display(Name = "Created At")]
        public string CreatedAt { get; set; } 

        [Display(Name = "Related Entity ID")]
        public string? RelatedEntityId { get; set; }
    }
}
