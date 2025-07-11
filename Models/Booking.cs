using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.Models
{
    public class Booking: BaseEntity
    {
        [Required(ErrorMessage = "User is required.")]
        [Display(Name = "User")]
        public string UserId { get; set; } 



        [Required(ErrorMessage = "Trip is required.")]
        [Display(Name = "Trip")]
        public int TripId { get; set; }


        [Required]
        [Display(Name = "Request Status")]
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;


        [Required(ErrorMessage = "Please enter the number of seats.")]
        [Range(1, 150, ErrorMessage = "Number of seats must be at least 1.")]
        [Display(Name = "Number of Seats")]
        public int NumberOfSeats { get; set; }

        //public bool IsPaid { get; set; } =false;
        //public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Price per seat is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Display(Name = "Price per Seat")]
        public decimal PricePerSeat { get; set; }

        public virtual Payment Payment { get; set; }

        [Required]
        [Display(Name = "Payment Status")]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        // Update TotalPrice to be stored in DB
        [Display(Name = "Total Price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice => PricePerSeat * NumberOfSeats;

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
