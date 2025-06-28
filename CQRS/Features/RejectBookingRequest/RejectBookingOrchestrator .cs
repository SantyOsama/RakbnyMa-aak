using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.RejectBookingRequest
{
    public class RejectBookingOrchestrator : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }
        public string CurrentUserId { get; set; }

        public RejectBookingOrchestrator(int bookingId, string currentUserId)
        {
            BookingId = bookingId;
            CurrentUserId = currentUserId;

        }
    }
}
