using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Created At")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Is Deleted")]
        [Required]
        public bool IsDeleted { get; set; } = false;

        [Timestamp]
        [Display(Name = "Row Version")]
        public byte[]? RowVersion { get; set; }
    }
}
