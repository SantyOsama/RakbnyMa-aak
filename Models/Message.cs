using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.Models
{
    public class Message : BaseEntity
    {
        [Required(ErrorMessage = "Trip is required.")]
        [Display(Name = "Trip")]
        public int TripId { get; set; }
        [Required(ErrorMessage = "Sender is required.")]
        [Display(Name = "Sender")]
        public string SenderId { get; set; }
        [Required(ErrorMessage = "Message content is required.")]
        [MaxLength(1000, ErrorMessage = "Message content must not exceed 1000 characters.")]
        [Display(Name = "Message Content")]
        public string Content { get; set; }
        [Display(Name = "Sent At")]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public virtual Trip Trip { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }


}
