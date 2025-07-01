using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.Models
{
    public class City : BaseEntity
    {
        [Required]
      
        public string Name { get; set; }
        public int GovernorateId { get; set; }
        public virtual Governorate Governorate { get; set; }
    }
}
