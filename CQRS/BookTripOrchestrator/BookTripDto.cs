namespace RakbnyMa_aak.CQRS.BookTripOrchestrator
{
    public class BookTripDto
    {
        public string UserId { get; set; }
        public int TripId { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
