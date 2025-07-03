using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.UpdateBookingStatus
{
    public record UpdateBookingStatusCommand(int BookingId, int TripId, RequestStatus NewStatus) : IRequest<Response<bool>>;

}
