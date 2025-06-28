using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.UpdateBookingStatus
{
    public class UpdateBookingStatusCommand : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }
        public int TripId { get; set; }
        public RequestStatus NewStatus { get; set; }

        public UpdateBookingStatusCommand(int bookingId, int tripId, RequestStatus newStatus)
        {
            BookingId = bookingId;
            TripId=tripId;
            NewStatus = newStatus;
        }
    }
}
