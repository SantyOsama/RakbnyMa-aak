using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Models
{
    public class Booking: BaseEntity
    {
        [Required(ErrorMessage = "User is required.")]
        [Display(Name = "User")]
        public string UserId { get; set; } // FK to ApplicationUser (the user who made the booking)
        [Required(ErrorMessage = "Trip is required.")]
        [Display(Name = "Trip")]
        public int TripId { get; set; } // FK to Trip (int, as Trip.Id is int)
        [Required]
        [Display(Name = "Request Status")]
        public RequestStatus RequestStatus { get; set; } //= RequestStatus.Pending;
        [Required(ErrorMessage = "Please enter the number of seats.")]
        [Range(1, 150, ErrorMessage = "Number of seats must be at least 1.")]
        [Display(Name = "Number of Seats")]
        public int NumberOfSeats { get; set; } // Renamed from NoOfSeat for clarity
        //public DateTime BookingDate { get; set; } = DateTime.UtcNow; // Added booking date // المفروض يتمسح عشان عندنا كريتيد ات    


        //public bool IsPaid { get; set; } =false;
        //public string PaymentMethod { get; set; }

        [Display(Name = "Total Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a positive number.")]
        public decimal? TotalPrice { get; set; }

        [Display(Name = "Has the trip started?")]
        public bool HasStarted { get; set; } = false;
        [Display(Name = "Has the trip ended?")]
        public bool HasEnded { get; set; } = false;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("TripId")]
        public virtual Trip Trip { get; set; }
    }
}
