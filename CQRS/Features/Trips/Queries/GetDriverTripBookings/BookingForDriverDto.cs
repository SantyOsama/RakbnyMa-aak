using System;
using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Features.Trip.Queries.GetDriverTripBookings
{
    public class BookingForDriverDto
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int TripId { get; set; }

        [Required]
        [Display(Name = "Passenger Name")]
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Booking Status")]
        public string Status { get; set; }

        [Display(Name = "Booking Request Date")]
        public DateTime RequestDate { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
