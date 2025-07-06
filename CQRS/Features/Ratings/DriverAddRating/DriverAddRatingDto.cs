using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Features.Ratings.DriverAddRating
{
    public class DriverAddRatingDto
    {
        [Required(ErrorMessage = "Trip ID is required.")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Rater ID is required.")]
        public string RaterId { get; set; }

        [Required(ErrorMessage = "Passenger ID to be rated is required.")]
        public string RatedPassengerId { get; set; }

        [MaxLength(500, ErrorMessage = "Comment must not exceed 500 characters.")]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "Rating value is required.")]
        [Range(1, 5, ErrorMessage = "Rating value must be between 1 and 5.")]
        public int RatingValue { get; set; }
    }
}
