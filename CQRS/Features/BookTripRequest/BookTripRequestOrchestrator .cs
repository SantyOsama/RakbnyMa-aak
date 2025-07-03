using MediatR;
using RakbnyMa_aak.CQRS.Commands.CreateBooking;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.BookTripRequest
{
    public record BookTripRequestOrchestrator(CreateBookingDto BookingDto) : IRequest<Response<int>>;
}
