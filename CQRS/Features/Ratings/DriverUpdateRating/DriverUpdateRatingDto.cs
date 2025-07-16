using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Features.Ratings.DriverUpdateRating
{
    public class DriverUpdateRatingDto
    {
        [Required(ErrorMessage = "Rating ID is required.")]
        public int RatingId { get; set; }

        public string? RaterId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating value must be between 1 and 5.")]
        public int? RatingValue { get; set; }

        [MaxLength(500, ErrorMessage = "Comment must not exceed 500 characters.")]
        public string? Comment { get; set; }
    }
}
