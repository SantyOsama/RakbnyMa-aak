using MediatR;
using RakbnyMa_aak.CQRS.CreateBooking;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.BookingOrchestrator
{
    public class BookTripRequestCommand : IRequest<Response<int>>
    {
        public CreateBookingDto BookingDto { get; set; }

        public BookTripRequestCommand(CreateBookingDto bookingDto)
        {
            BookingDto = bookingDto;
        }
    }
}
