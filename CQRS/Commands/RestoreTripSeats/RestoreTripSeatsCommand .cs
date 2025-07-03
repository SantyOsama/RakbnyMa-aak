using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Commands.RestoreTripSeats
{
    public record RestoreTripSeatsCommand(Trip Trip, int SeatsToRestore) : IRequest<Response<bool>>;
}
