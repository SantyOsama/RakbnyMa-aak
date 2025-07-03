using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Governorates
{
    public class GovernorateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Governorate name is required.")]
        [MaxLength(50, ErrorMessage = "Governorate name must not exceed 50 characters.")]
        [Display(Name = "Governorate Name")]
        public string Name { get; set; }
    }
}
