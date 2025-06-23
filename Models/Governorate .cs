using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.Models
{
    public class Governorate:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
