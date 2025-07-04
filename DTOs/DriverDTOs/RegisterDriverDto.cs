using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.DTOs.DriverDTOs
{
    public class RegisterDriverDto
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please enter your full name.")]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Full name must not exceed 100 characters.")]
        public string FullName { get; set; }



        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$",
           ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }



        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirmation password is required.")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }



        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$", ErrorMessage = "Phone number must be 11 digits and start with 010, 011, 012, or 015.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please select a gender.")]
        [EnumDataType(typeof(Gender))]
        [Column(TypeName = "nvarchar(20)")]
        public Gender Gender { get; set; }



        [Display(Name = "National ID")]
        [Required(ErrorMessage = "National ID is required.")]
        [RegularExpression(@"^[23]\d{13}$", ErrorMessage = "National ID must start with 2 or 3 and be exactly 14 digits.")]
        public string NationalId { get; set; }



        [Display(Name = "Car Type")]
        [Required(ErrorMessage = "Car type is required.")]
        public CarType CarType { get; set; }



        [Required(ErrorMessage = "Car model is required. ex(Kia Cerato 2023)")]
        [StringLength(50, ErrorMessage = "Car model must not exceed 50 characters. ex(Kia Cerato 2023)")]
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }



        [Required(ErrorMessage = "Car color is required.")]
        [Display(Name = "Car Color")]
        public CarColor CarColor { get; set; }



        [Display(Name = "Car Capacity")]
        [Required(ErrorMessage = "Car capacity is required.")]
        [Range(2, 150, ErrorMessage = "Car capacity must be between 2 and 150.")]
        public int CarCapacity { get; set; }



        [Display(Name = "National ID Image")]
        [Required(ErrorMessage = "National ID image is required.")]
        public IFormFile NationalIdImage { get; set; }



        [Display(Name = "Driver License Image")]
        [Required(ErrorMessage = "Driver license image is required.")]
        public IFormFile DriverLicenseImage { get; set; }



        [Display(Name = "Car License Image")]
        [Required(ErrorMessage = "Car license image is required.")]
        public IFormFile CarLicenseImage { get; set; }



        [Display(Name = "Selfie Image")]
        [Required(ErrorMessage = "Selfie image is required.")]
        public IFormFile SelfieImage { get; set; }


        [Display(Name = "Profile Picture")]
        public IFormFile? Picture { get; set; }
    }
}
