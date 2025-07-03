using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RakbnyMa_aak.Models
{
    public class TripTracking :BaseEntity
    {
        [Required(ErrorMessage = "Trip ID is required.")]
        [Display(Name = "Trip")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "Current latitude is required.")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        [Display(Name = "Current Latitude")]
        public double CurrentLat { get; set; }

        [Required(ErrorMessage = "Current longitude is required.")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        [Display(Name = "Current Longitude")]
        public double CurrentLong { get; set; }

        [Required(ErrorMessage = "Timestamp is required.")]
        [Display(Name = "Timestamp")]
        public DateTime Timestamp { get; set; }

        // Navigation property
        [ForeignKey("TripId")]
        [Display(Name = "Trip")]
        public virtual Trip Trip { get; set; }
    }
}
