using System.ComponentModel.DataAnnotations;

namespace RakbnyMa_aak.CQRS.Features.Ratings.CommonAddRating
{
    public class CommonAddRatingDto
    {
        [Required(ErrorMessage = "Trip ID is required.")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Rater ID is required.")]
        public string RaterId { get; set; }

        [Required(ErrorMessage = "Rated ID is required.")]
        public string RatedId { get; set; }

        [Required(ErrorMessage = "Rating value is required.")]
        [Range(1, 5, ErrorMessage = "Rating value must be between 1 and 5.")]
        public int RatingValue { get; set; }

        [MaxLength(500, ErrorMessage = "Comment must not exceed 500 characters.")]
        public string? Comment { get; set; }

        [Required(ErrorMessage = "Please specify the rating type.")]
        [Display(Name = "Is Driver Rating")]
        public bool IsDriverRating { get; set; } // True: driver rating passenger | False: passenger rating driver
    }
}
