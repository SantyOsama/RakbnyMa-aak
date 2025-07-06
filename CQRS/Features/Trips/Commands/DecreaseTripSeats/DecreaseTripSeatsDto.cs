using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Features.Trip.Commands.DecreaseTripSeats
{
    public class DecreaseTripSeatsDto
    {
        [Required(ErrorMessage = "Trip ID is required.")]
        [Display(Name = "Trip ID")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Number of seats is required.")]
        [Range(1, 150, ErrorMessage = "Number of seats must be at least 1.")]
        [Display(Name = "Number of Seats to Decrease")]
        public int NumberOfSeats { get; set; }
    }
}
