using System.ComponentModel.DataAnnotations.Schema;

namespace RakbnyMa_aak.Models
{
    public class TripTracking :BaseEntity
    {
        public int TripId { get; set; } 
        public double CurrentLat { get; set; }
        public double CurrentLong { get; set; }
        public DateTime Timestamp { get; set; } 

        // Navigation property
        [ForeignKey("TripId")]
        public virtual Trip Trip { get; set; }
    }
}
