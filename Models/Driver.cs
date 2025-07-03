using System.ComponentModel.DataAnnotations;
using static RakbnyMa_aak.Enums.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RakbnyMa_aak.Models
{
    public class Driver:BaseEntity
    {
        [Key]
        [ForeignKey("ApplicationUser")] // UserId is PK and FK to ApplicationUser
        public string UserId { get; set; }
        [Required(ErrorMessage = "National ID is required.")]
        [RegularExpression(@"^[2-3]\d{13}$", ErrorMessage = "National ID must be 14 digits and start with 2 or 3.")]
        [Display(Name = "National ID")]
        public string NationalId { get; set; }
        // Changed Enum CarModel to CarType based on previous ERD and common practice
        [Required(ErrorMessage = "Car type is required.")]
        [Display(Name = "Car Type")]
        public CarType CarType { get; set; }

        [Required(ErrorMessage = "Car model is required. ex(Kia Cerato 2023)")]
        [StringLength(50, ErrorMessage = "Car model must not exceed 50 characters. ex(Kia Cerato 2023)")]
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "Car color is required.")]
        [StringLength(30, ErrorMessage = "Car color must not exceed 30 characters.")]
        [Display(Name = "Car Color")]
        public string CarColor { get; set; }

        [Required(ErrorMessage = "Car capacity is required.")]
        [Range(2, 150, ErrorMessage = "Car capacity must be at least 2.")]
        [Display(Name = "Car Capacity")]
        public int CarCapacity { get; set; } // Added based on ERD discussion

        [Required(ErrorMessage = "National ID image is required.")]
        [Display(Name = "National ID Image")]
        public string DriverNationalIdImagePath { get; set; }
        [Required(ErrorMessage = "Driver license image is required.")]
        [Display(Name = "Driver License Image")]
        public string DriverLicenseImagePath { get; set; }
        [Required(ErrorMessage = "Car license image is required.")]
        [Display(Name = "Car License Image")]
        public string CarLicenseImagePath { get; set; }
        [Required(ErrorMessage = "Selfie image is required.")]
        [Display(Name = "Selfie Image")]
        public string SelfieImagePath { get; set; }
        [Display(Name = "Face Verified")]
        public bool IsFaceVerified { get; set; } = false;
        [Display(Name = "Phone Verified")]
        public bool IsPhoneVerified { get; set; } = false;
        [Display(Name = "Approved")]
        public bool IsApproved { get; set; } = false; // For admin approval
        [Display(Name = "Phone Verification Code")]
        public string? PhoneVerificationCode { get; set; }

        // Navigation property back to ApplicationUser
        public virtual ApplicationUser User { get; set; }

        // Driver has many Trips
        public virtual ICollection<Trip>? Trips { get; set; }

        public Driver()
        {
            Trips = new HashSet<Trip>();
        }

    }
}
