using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RakbnyMa_aak.Models
{
    public class City : BaseEntity
    {
        [Required(ErrorMessage = "City name is required.")]
        [MaxLength(100, ErrorMessage = "City name must not exceed 100 characters.")]
        [Display(Name = "City Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Governorate is required.")]
        [Display(Name = "Governorate")]
        public int GovernorateId { get; set; }
        [ForeignKey("GovernorateId")]
        public virtual Governorate Governorate { get; set; }
    }
}
