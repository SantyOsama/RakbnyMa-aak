using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.BookValidationOrchestrator
{
    public class BookingValidationResultDto
    {
        public int BookingId { get; set; }
        public int TripId { get; set; }
        public string PassengerId { get; set; }
        public string DriverId { get; set; }
        public int NumberOfSeats { get; set; }
        public RequestStatus requestStatus { get; set; }
    }

}
