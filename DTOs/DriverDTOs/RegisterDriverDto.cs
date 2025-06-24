using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.DTOs.DriverDTOs
{
    public class RegisterDriverDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Full name must not exceed 100 characters.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone number must be 11 digits and start with 010, 011, 012, or 015.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "National ID is required.")]
        [RegularExpression(@"^[23]\d{13}$", ErrorMessage = "National ID must start with 2 or 3 and be 14 digits.")]
        public string NationalId { get; set; }

        [Required]
        public CarType CarType { get; set; }

        [Required]
        [Range(1, 14, ErrorMessage = "Car capacity must be between 1 and 14.")]
        public int CarCapacity { get; set; }

        [Required(ErrorMessage = "National ID image is required.")]
        public IFormFile NationalIdImage { get; set; }

        [Required(ErrorMessage = "Driver license image is required.")]
        public IFormFile DriverLicenseImage { get; set; }

        [Required(ErrorMessage = "Car license image is required.")]
        public IFormFile CarLicenseImage { get; set; }

        [Required(ErrorMessage = "Selfie image is required.")]
        public IFormFile SelfieImage { get; set; }
    }
}
