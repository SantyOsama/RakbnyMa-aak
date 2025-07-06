using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.Models
{
    public class Trip:BaseEntity
    {
        // Id is inherited from BaseEntity (int PK)
        [Required(ErrorMessage = "Driver ID is required.")]
        [Display(Name = "Driver ID")]
        public string DriverId { get; set; } // FK to Driver (string, as Driver.UserId is string)

        // City and Governorate can be strings or separate lookup entities
        // Keeping them as strings for now as per the ERD's attributes

        [Required]
        [Display(Name = "From City")]
        public int FromCityId { get; set; }


        [Required]
        [Display(Name = "To City")]
        public int ToCityId { get; set; }


        [Required]
        [Display(Name = "From Governorate")]
        public int FromGovernorateId { get; set; }


        [Required]
        [Display(Name = "To Governorate")]
        public int ToGovernorateId { get; set; }


        [Required(ErrorMessage = "Pickup location is required.")]
        [Display(Name = "Pickup Location")]
        public string PickupLocation { get; set; }


        [Required(ErrorMessage = "Destination location is required.")]
        [Display(Name = "Destination Location")]
        public string DestinationLocation { get; set; }


        [Required(ErrorMessage = "Trip date is required.")]
        [Display(Name = "Trip Date")]
        public DateTime TripDate { get; set; }


        [Required(ErrorMessage = "Available seats are required.")]
        [Range(1, 150, ErrorMessage = "Available seats must be at least 1.")]
        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }


        [Required(ErrorMessage = "Price per seat is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Display(Name = "Price per Seat")]
        public decimal PricePerSeat { get; set; }


        [Required]
        [Display(Name = "Trip Status")]
        public TripStatus TripStatus { get; set; }


        [Display(Name = "Is Recurring?")]
        public bool IsRecurring { get; set; } = false;


        [Display(Name = "Women Only?")]
        public bool WomenOnly { get; set; } = false;


        [Display(Name = "From City")]
        public City FromCity { get; set; }


        [Display(Name = "To City")]
        public City ToCity { get; set; }


        [Display(Name = "From Governorate")]
        public Governorate FromGovernorate { get; set; }


        [Display(Name = "To Governorate")]
        public Governorate ToGovernorate { get; set; }


        // Navigation property to Driver
        [ForeignKey("DriverId")]
        [Display(Name = "Driver")]
        public virtual Driver Driver { get; set; }

        // Trip has many Bookings
        [Display(Name = "Bookings")]
        public virtual ICollection<Booking> Bookings { get; set; }

        // Trip has many Ratings (ratings for this specific trip)
        [Display(Name = "Ratings")]
        public virtual ICollection<Rating> Ratings { get; set; }

        // Trip has many Tracking points
        [Display(Name = "Trip Trackings")]
        public virtual ICollection<TripTracking> TripTrackings { get; set; }

        public Trip()
        {
            Bookings = new HashSet<Booking>();
            Ratings = new HashSet<Rating>();
            TripTrackings = new HashSet<TripTracking>();
        }

    }
}
