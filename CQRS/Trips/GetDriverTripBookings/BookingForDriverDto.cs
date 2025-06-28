namespace RakbnyMa_aak.CQRS.Trips.GetDriverTripBookings
{
    public class BookingForDriverDto
    {
        public int BookingId { get; set; }
        public int TripId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public string UserId { get; internal set; }
    }
}
