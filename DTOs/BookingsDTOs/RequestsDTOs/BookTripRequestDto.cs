using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.BookingsDTOs.Requests
{
    public class BookTripRequestDto
    {
        //public string? UserId { get; set; }

        [Required(ErrorMessage = "Trip ID is required.")]
        [Display(Name = "Trip ID")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Number of seats is required.")]
        [Range(1, 150, ErrorMessage = "Number of seats must be between 1 and 150.")]
        [Display(Name = "Number of Seats")]
        public int NumberOfSeats { get; set; }

    }
}
