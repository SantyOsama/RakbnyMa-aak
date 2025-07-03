using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.PersistTrip
{
    public record PersistTripCommand(TripDto TripDto) : IRequest<Response<int>>;
}
