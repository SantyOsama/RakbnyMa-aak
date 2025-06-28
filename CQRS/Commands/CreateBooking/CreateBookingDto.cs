namespace RakbnyMa_aak.CQRS.Commands.CreateBooking
{
    public class CreateBookingDto
    {
        public int TripId { get; set; }
        public string UserId { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
