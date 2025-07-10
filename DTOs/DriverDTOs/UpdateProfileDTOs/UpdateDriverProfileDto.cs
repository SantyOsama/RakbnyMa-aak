using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.DriverDTOs.UpdateProfileDTOs
{
    public class UpdateDriverProfileDto
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please enter your full name.")]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "Full name must not exceed 100 characters.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be 11 digits and start with 010, 011, 012, or 015.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile? Picture { get; set; }

        [Display(Name = "Selfie Image")]
        [Required(ErrorMessage = "Selfie image is required.")]
        public IFormFile SelfieImage { get; set; }

        [Display(Name = "National ID Image")]
        [Required(ErrorMessage = "National ID image is required.")]
        public IFormFile NationalIdImage { get; set; }
    }
}
