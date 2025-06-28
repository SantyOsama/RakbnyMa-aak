namespace RakbnyMa_aak.CQRS.Commands.Validations.CheckUserAlreadyBooked
{
    public class CheckUserAlreadyBookedDto
    {
        public int TripId { get; set; }
        public string UserId { get; set; }
    }
}
