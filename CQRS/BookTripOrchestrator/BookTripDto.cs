namespace RakbnyMa_aak.CQRS.BookTripOrchestrator
{
    public class BookTripDto
    {
        public string PassengerUserId { get; set; }
        public int TripId { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
