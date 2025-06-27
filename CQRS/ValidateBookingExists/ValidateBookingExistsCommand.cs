using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.ValidateBookingExists
{
    public class ValidateBookingExistsCommand : IRequest<Response<Booking>>
    {
        public int BookingId { get; set; }

        public ValidateBookingExistsCommand(int bookingId)
        {
            BookingId = bookingId;
        }
    }

}
