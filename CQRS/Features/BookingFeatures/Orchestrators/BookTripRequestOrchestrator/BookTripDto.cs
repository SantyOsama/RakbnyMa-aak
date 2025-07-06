using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Features.Booking.Orchestrators.BookTripRequest
{
    public class BookTripDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Trip ID is required.")]
        [Display(Name = "Trip ID")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Number of seats is required.")]
        [Range(1, 150, ErrorMessage = "Number of seats must be between 1 and 150.")]
        [Display(Name = "Number of Seats")]
        public int NumberOfSeats { get; set; }

        [Required(ErrorMessage = "Price per seat is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Display(Name = "Price per Seat")]
        public decimal PricePerSeat { get; set; }
    }
}
