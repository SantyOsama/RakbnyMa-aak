using System;
using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Ratings.DriverGetRatings
{
    public class DriverRatingDto
    {
        public int RatingId { get; set; }

        [Required(ErrorMessage = "Trip ID is required.")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Rating value is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int RatingValue { get; set; }

        [MaxLength(500, ErrorMessage = "Comment must not exceed 500 characters.")]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "Rater name is required.")]
        [MaxLength(100, ErrorMessage = "Rater name must not exceed 100 characters.")]
        public string RaterName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
