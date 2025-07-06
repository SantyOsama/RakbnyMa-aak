using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Features.Cities
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City name is required.")]
        [StringLength(50, ErrorMessage = "City name must not exceed 50 characters.")]
        [Display(Name = "City Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Governorate ID is required.")]
        [Display(Name = "Governorate ID")]
        public int GovernorateId { get; set; }

        [Display(Name = "Governorate Name")]
        public string? GovernorateName { get; internal set; }
    }
}
