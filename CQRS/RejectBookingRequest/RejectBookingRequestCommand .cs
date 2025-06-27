using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.BookingOrchestrators
{
    public class RejectBookingRequestCommand : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }
        public string CurrentUserId { get; set; }

        public RejectBookingRequestCommand(int bookingId, string currentUserId)
        {
            BookingId = bookingId;
            CurrentUserId = currentUserId;

        }
    }
}
