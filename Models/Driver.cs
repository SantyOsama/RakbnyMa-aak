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

        public string NationalId { get; set; }
        // Changed Enum CarModel to CarType based on previous ERD and common practice
        public CarType CarType { get; set; }
        public int CarCapacity { get; set; } // Added based on ERD discussion

        public string DriverNationalIdImagePath { get; set; }
        public string DriverLicenseImagePath { get; set; }
        public string CarLicenseImagePath { get; set; }
        public string SelfieImagePath { get; set; }

        public bool IsFaceVerified { get; set; } = false;
        public bool IsPhoneVerified { get; set; } = false;
        public bool IsApproved { get; set; } = false; // For admin approval
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
