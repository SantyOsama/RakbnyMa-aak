using Microsoft.AspNetCore.Identity;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public string Picture { get; set; }
        public UserType UserType { get; set; }
        public virtual Driver? Driver { get; set; }
        public virtual Passenger? Passenger { get; set; }
        public virtual ICollection<Rating>? RatingsGiven { get; set; }
        public virtual ICollection<Rating>? RatingsReceived { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }

        // User can send/receive many messages
        public virtual ICollection<Message>? SentMessages { get; set; }
        public virtual ICollection<Message>? ReceivedMessages { get; set; }

        public ApplicationUser()
        {
            RatingsGiven = new HashSet<Rating>();
            RatingsReceived = new HashSet<Rating>();
            Bookings = new HashSet<Booking>();
            SentMessages = new HashSet<Message>();
            ReceivedMessages = new HashSet<Message>();
        }

    }
}
