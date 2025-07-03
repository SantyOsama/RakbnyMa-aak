using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Queries.GetUserRatings
{
    public class UserRatingDto
    {
        [Required]
        public int RatingId { get; set; }

        [Required]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Driver name is required.")]
        [MaxLength(100, ErrorMessage = "Driver name must not exceed 100 characters.")]
        public string DriverName { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int RatingValue { get; set; }

        [MaxLength(500, ErrorMessage = "Comment must not exceed 500 characters.")]
        public string? Comment { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
