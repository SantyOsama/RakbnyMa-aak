namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists
{
    public class TripValidationResultDto
    {
        public int TripId { get; set; }
        public string DriverId { get; set; }
        public DateTime TripDate { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsDeleted { get; set; }
    }

}
