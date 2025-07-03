using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.Models
{
    public class Governorate:BaseEntity
    {
        [Required(ErrorMessage = "Governorate name is required.")]
        [MaxLength(100, ErrorMessage = "Governorate name must not exceed 100 characters.")]
        [Display(Name = "Governorate Name")]
        public string Name { get; set; }

        [Display(Name = "Cities")]
        public virtual ICollection<City> Cities { get; set; }

        public Governorate() 
        {
            Cities = new HashSet<City>();
        }
    }
}
