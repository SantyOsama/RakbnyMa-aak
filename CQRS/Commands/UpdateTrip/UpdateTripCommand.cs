using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.UpdateTrip
{
    public record UpdateTripCommand(int TripId, TripDto TripDto) : IRequest<Response<int>>;

}
