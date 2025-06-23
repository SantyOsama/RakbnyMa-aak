using System.ComponentModel.DataAnnotations.Schema;

namespace RakbnyMa_aak.Models
{
    public class Rating : BaseEntity
    {
        // Id is inherited from BaseEntity (int PK)

        public int TripId { get; set; } // FK to Trip
        public string RaterId { get; set; } // FK to ApplicationUser (the one who gives the rating)
        public string RatedId { get; set; } // FK to ApplicationUser (the one who receives the rating)

        public int RatingValue { get; set; } // Renamed from RatingNum for clarity (e.g., 1-5)
        public string Comment { get; set; }

        // Navigation properties
        [ForeignKey("TripId")]
        public virtual Trip Trip { get; set; }

        [ForeignKey("RaterId")]
        public virtual ApplicationUser Rater { get; set; }

        [ForeignKey("RatedId")]
        public virtual ApplicationUser Rated { get; set; }

        // Business Logic Constraint Support:
        // This model structure supports the constraint. The actual enforcement
        // (checking if RaterId and RatedId were part of TripId) will happen in the
        // Business Logic Layer (e.g., a RatingService) before saving the rating.
        // You would query the Trip's bookings and driver to verify participation.
    }
}
