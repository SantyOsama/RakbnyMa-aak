using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.UserDTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirmation Passwoed don't match.!!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //[Required]
        //public string UserType { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone number must be 11 digits and start with 010, 011, 012, or 015.")]


        public string PhoneNumber { get; set; }

        public IFormFile? Picture { get; set; }
    }
}