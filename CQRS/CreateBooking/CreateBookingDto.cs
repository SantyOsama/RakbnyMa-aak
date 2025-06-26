namespace RakbnyMa_aak.CQRS.CreateBooking
{
    public class CreateBookingDto
    {
        public int TripId { get; set; }
        public String UserId { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
