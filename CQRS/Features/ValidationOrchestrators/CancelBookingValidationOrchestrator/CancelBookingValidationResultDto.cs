using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.CancelBookingValidationOrchestrator
{
    public class CancelBookingValidationResultDto
    {
        public int BookingId { get; set; }
        public int TripId { get; set; }
        public int NumberOfSeats { get; set; }
        public bool WasConfirmed { get; set; }
    }

}
