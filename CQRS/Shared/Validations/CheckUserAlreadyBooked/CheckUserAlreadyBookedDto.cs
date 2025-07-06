using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Commands.Validations.CheckUserAlreadyBooked
{
    public class CheckUserAlreadyBookedDto
    {
        [Required(ErrorMessage = "Trip ID is required.")]
        [Display(Name = "Trip ID")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }
    }
}
