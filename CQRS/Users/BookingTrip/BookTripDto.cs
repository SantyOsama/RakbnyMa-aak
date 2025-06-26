namespace RakbnyMa_aak.CQRS.Users.BookingTrip
{
    public class BookTripDto
    {
        public int TripId { get; set; }
        public int NumberOfSeats { get; set; }
        public string PassengerUserId { get; set; } = null!;
        public string PaymentMethod { get; set; } = "Cash";
    }
}
