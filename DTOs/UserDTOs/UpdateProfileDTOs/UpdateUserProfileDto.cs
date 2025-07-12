using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.UserDTOs.UpdateProfileDTOs
{
    public class UpdateUserProfileDto
    {
        [Required(ErrorMessage = "Full name is required.")]
        [Display(Name = "Full Name")]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Full name must not exceed 100 characters.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$",
            ErrorMessage = "Phone number must be 11 digits and start with 010, 011, 012, or 015.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Profile Picture")]
        public IFormFile? Picture { get; set; }
    }
}
