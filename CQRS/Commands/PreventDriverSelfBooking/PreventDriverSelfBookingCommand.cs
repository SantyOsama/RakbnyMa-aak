using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.PreventDriverSelfBooking
{
    public record PreventDriverSelfBookingCommand(string DriverId, string UserId) : IRequest<Response<string>>;

}
