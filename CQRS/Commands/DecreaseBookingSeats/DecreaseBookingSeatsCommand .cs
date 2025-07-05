using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.DecreaseBookingSeats
{
    public record DecreaseBookingSeatsCommand(int BookingId, int SeatsToRemove) : IRequest<Response<int>>;

}
