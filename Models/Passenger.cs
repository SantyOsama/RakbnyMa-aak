using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RakbnyMa_aak.Models
{
    public class Passenger : BaseEntity
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }

        public Passenger()
        {
            Bookings = new HashSet<Booking>();
        }
    }
}
