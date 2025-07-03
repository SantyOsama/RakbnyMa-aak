namespace RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.BookValidationOrchestrator
{
    public class BookingValidationResultDto
    {
        public int BookingId { get; set; }
        public int TripId { get; set; }
        public string PassengerId { get; set; }
        public string DriverId { get; set; }
    }

}
