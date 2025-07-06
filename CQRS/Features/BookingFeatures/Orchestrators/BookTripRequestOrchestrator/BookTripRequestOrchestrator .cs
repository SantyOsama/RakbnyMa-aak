using MediatR;
using RakbnyMa_aak.DTOs.BookingsDTOs.Requests;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.Booking.Orchestrators.BookTripRequest
{
    public record BookTripRequestOrchestrator(CreateBookingRequestDto BookingDto) : IRequest<Response<int>>;
}
