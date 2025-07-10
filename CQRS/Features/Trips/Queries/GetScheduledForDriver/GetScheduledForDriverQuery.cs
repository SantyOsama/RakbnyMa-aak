using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs.RequestsDTOs;
using RakbnyMa_aak.DTOs.TripDTOs.ResponseDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.Trips.Queries.GetScheduledForDriver
{
    public record GetScheduledForDriverQuery(ScheduledTripRequestDto Filter, string DriverId)
         : IRequest<Response<PaginatedResult<TripResponseDto>>>;
}
