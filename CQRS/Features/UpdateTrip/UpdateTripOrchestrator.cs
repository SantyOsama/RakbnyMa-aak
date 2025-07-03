using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.UpdateTrip
{
    public record UpdateTripOrchestrator(int TripId,string CurrentUserId, TripDto TripDto) : IRequest<Response<int>>;

}
