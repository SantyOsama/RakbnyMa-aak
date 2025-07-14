using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.TripDTOs
{
    public class CreateAndUpdateTripDto
    {
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
        [DataType(DataType.DateTime)]
        [Display(Name = "Trip Date")]
        public DateTime TripDate { get; set; }


        [Required(ErrorMessage = "Available seats is required.")]
        [Range(1, 150, ErrorMessage = "Available seats must be between 1 and 150.")]
        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }

        [Required(ErrorMessage = "Price per seat is required.")]
        [Range(1, 10000, ErrorMessage = "Price must be greater than 0.")]
        [Display(Name = "Price Per Seat")]
        public decimal PricePerSeat { get; set; }

        [Display(Name = "Is Recurring?")]
        public bool IsRecurring { get; set; }

        [Display(Name = "Women Only?")]
        public bool WomenOnly { get; set; }
    }
}
