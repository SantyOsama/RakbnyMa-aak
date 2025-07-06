using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.Auth.Requests
{
    public class LoginRequestDto
    {
        [Display(Name = "Email or Username")]
        [Required(ErrorMessage = "Please enter your email or username.")]
        public string EmailOrUsername { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
