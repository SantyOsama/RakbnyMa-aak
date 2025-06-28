namespace RakbnyMa_aak.CQRS.Commands.DecreaseTripSeats
{
    public class DecreaseTripSeatsDto
    {
        public int TripId { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
