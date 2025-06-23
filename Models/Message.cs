namespace RakbnyMa_aak.Models
{
    public class Message : BaseEntity
    {
        public int TripId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public virtual Trip Trip { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }


}
