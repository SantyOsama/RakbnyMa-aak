using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.UserDTOs
{
    public class RegisterUserDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

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
        public string ConfirmPassword { get; set; }

        //[Required]
        //public string UserType { get; set; }

        public string PhoneNumber { get; set; }

        public IFormFile? Picture { get; set; }
    }
}