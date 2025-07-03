using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.CancelBookingByPassenger
{
    public record CancelBookingByPassengerCommand(int BookingId, string CurrentUserId) : IRequest<Response<bool>>;

}
