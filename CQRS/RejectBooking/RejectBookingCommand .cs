using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.RejectBooking
{
    public class RejectBookingCommand : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }

        public RejectBookingCommand(int bookingId)
        {
            BookingId = bookingId;
        }
    }
}
