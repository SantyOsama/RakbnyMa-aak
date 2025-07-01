using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.IncreaseBookingSeats
{
    public record IncreaseBookingSeatsCommand(int BookingId, int SeatsToAdd) : IRequest<Response<int>>;
}
