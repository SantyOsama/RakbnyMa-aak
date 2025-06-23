using System.ComponentModel.DataAnnotations.Schema;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Models
{
    public class Booking: BaseEntity
    {
        public string UserId { get; set; } // FK to ApplicationUser (the user who made the booking)
        public int TripId { get; set; } // FK to Trip (int, as Trip.Id is int)

        public RequestStatus RequestStatus { get; set; }
        public int NumberOfSeats { get; set; } // Renamed from NoOfSeat for clarity
        public DateTime BookingDate { get; set; } = DateTime.UtcNow; // Added booking date

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("TripId")]
        public virtual Trip Trip { get; set; }
    }
}
