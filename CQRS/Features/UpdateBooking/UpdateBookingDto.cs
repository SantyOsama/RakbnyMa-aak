using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Features.UpdateBooking
{
    public class UpdateBookingDto
    {
        [Required(ErrorMessage = "Trip ID is required.")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Booking ID is required.")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Number of seats is required.")]
        [Range(1, 150, ErrorMessage = "Number of seats must be between 1 and 150.")]
        public int NewNumberOfSeats { get; set; }
    }

}
