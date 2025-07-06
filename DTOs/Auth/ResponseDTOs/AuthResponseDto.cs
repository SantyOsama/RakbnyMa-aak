using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.Auth.Responses
{
    public class AuthResponseDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Token is required.")]
        [Display(Name = "JWT Token")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [Display(Name = "User Role")]
        public string Role { get; set; }
    }
}
