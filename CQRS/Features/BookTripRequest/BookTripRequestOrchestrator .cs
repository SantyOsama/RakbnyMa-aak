using MediatR;
using RakbnyMa_aak.CQRS.Commands.CreateBooking;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.BookTripRequest
{
    public class BookTripRequestOrchestrator : IRequest<Response<int>>
    {
        public CreateBookingDto BookingDto { get; set; }

        public BookTripRequestOrchestrator(CreateBookingDto bookingDto)
        {
            BookingDto = bookingDto;
        }
    }
}
