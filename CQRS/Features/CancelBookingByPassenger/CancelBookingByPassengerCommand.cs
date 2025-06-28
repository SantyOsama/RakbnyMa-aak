using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.CancelBookingByPassenger
{
    public class CancelBookingByPassengerCommand : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }
        public string CurrentUserId { get; set; }

        public CancelBookingByPassengerCommand(int bookingId, string currentUserId)
        {
            BookingId = bookingId;
            CurrentUserId = currentUserId;
        }
    }

}
