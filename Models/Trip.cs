using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Models
{
    public class Trip:BaseEntity
    {
        // Id is inherited from BaseEntity (int PK)
        public string DriverId { get; set; } // FK to Driver (string, as Driver.UserId is string)

        // City and Governorate can be strings or separate lookup entities
        // Keeping them as strings for now as per the ERD's attributes

        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public int FromGovernorateId { get; set; }
        public int ToGovernorateId { get; set; }

        public string PickupLocation { get; set; }
        public string DestinationLocation { get; set; }
        public DateTime TripDate { get; set; } 
        public TimeSpan Duration { get; set; } 
        public int AvailableSeats { get; set; }
        public decimal PricePerSeat { get; set; }
        public TripStatus TripStatus { get; set; }
        public bool IsRecurring { get; set; } = false;
        public bool WomenOnly { get; set; } = false;



        public City FromCity { get; set; }
        public City ToCity { get; set; }
        public Governorate FromGovernorate { get; set; }
        public Governorate ToGovernorate { get; set; }


        // Navigation property to Driver
        [ForeignKey("DriverId")]
        public virtual Driver? Driver { get; set; }

        // Trip has many Bookings
        public virtual ICollection<Booking>? Bookings { get; set; }

        // Trip has many Ratings (ratings for this specific trip)
        public virtual ICollection<Rating>? Ratings { get; set; }

        // Trip has many Tracking points
        public virtual ICollection<TripTracking>? TripTrackings { get; set; }

        public Trip()
        {
            Bookings = new HashSet<Booking>();
            Ratings = new HashSet<Rating>();
            TripTrackings = new HashSet<TripTracking>();
        }

    }
}
