using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.UpdateBooking
{
    public record UpdateBookingOrchestrator(UpdateBookingDto BookingDto)
    : IRequest<Response<bool>>;
}
