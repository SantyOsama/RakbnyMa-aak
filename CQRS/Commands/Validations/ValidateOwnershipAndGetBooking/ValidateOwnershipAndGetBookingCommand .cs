using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateOwnershipAndGetBooking
{
    public record ValidateOwnershipAndGetBookingCommand(int BookingId, string UserId) : IRequest<Response<Booking>>;

}
