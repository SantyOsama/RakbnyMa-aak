using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.RestoreTripSeats
{
    public record RestoreTripSeatsCommand(int TripId, int SeatsToRestore) : IRequest<Response<bool>>;

}
