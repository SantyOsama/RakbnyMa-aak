using static RakbnyMa_aak.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.DTOs.DriverDTOs.UpdateProfileDTOs
{
    public class UpdateDriverCarInfoDto
    {
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

        [Required(ErrorMessage = "Car plate number is required.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Car plate number must be between 2 and 10 characters.")]
        public string CarPlateNumber { get; set; }


        [Display(Name = "Driver License Image")]
        [Required(ErrorMessage = "Driver license image is required.")]
        public IFormFile DriverLicenseImage { get; set; }

        [Display(Name = "Car License Image")]
        [Required(ErrorMessage = "Car license image is required.")]
        public IFormFile CarLicenseImage { get; set; }
    }
}
