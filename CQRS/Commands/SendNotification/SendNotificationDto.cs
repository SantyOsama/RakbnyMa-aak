using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Commands.SendNotification
{
    public class SendNotificationDto
    {
        [Required(ErrorMessage = "Receiver ID is required.")]
        [Display(Name = "Receiver ID")]
        public string ReceiverId { get; set; }

        [Required(ErrorMessage = "Sender ID is required.")]
        [Display(Name = "Sender ID")]
        public string SenderUserId { get; set; }

        [Required(ErrorMessage = "Message content is required.")]
        [StringLength(500, ErrorMessage = "Message must not exceed 500 characters.")]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
