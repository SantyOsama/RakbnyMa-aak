using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.ApproveBooking
{
    public class ApproveBookingCommand : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }

        public ApproveBookingCommand(int bookingId)
        {
            BookingId = bookingId;
        }
    }
}
