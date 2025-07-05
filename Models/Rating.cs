using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RakbnyMa_aak.Models
{
    public class Rating : BaseEntity
    {
        // Id is inherited from BaseEntity (int PK)
        [Required(ErrorMessage = "Trip ID is required.")]
        [Display(Name = "Trip")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Rater ID is required.")]
        [Display(Name = "Rater")]
        public string RaterId { get; set; }



        [Required(ErrorMessage = "Rated ID is required.")]
        [Display(Name = "Rated")]
        public string RatedId { get; set; } 



        [Required(ErrorMessage = "Rating value is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        [Display(Name = "Rating Value")]
        public int RatingValue { get; set; }



        [MaxLength(1000, ErrorMessage = "Comment must not exceed 1000 characters.")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }


        // Navigation properties
        [ForeignKey("TripId")]
        [Display(Name = "Trip")]
        public virtual Trip Trip { get; set; }

        [ForeignKey("RaterId")]
        [Display(Name = "Rater")]
        public virtual ApplicationUser Rater { get; set; }

        [ForeignKey("RatedId")]
        [Display(Name = "Rated")]
        public virtual ApplicationUser Rated { get; set; }

        // Business Logic Constraint Support:
        // This model structure supports the constraint. The actual enforcement
        // (checking if RaterId and RatedId were part of TripId) will happen in the
        // Business Logic Layer (e.g., a RatingService) before saving the rating.
        // You would query the Trip's bookings and driver to verify participation.
    }
}
