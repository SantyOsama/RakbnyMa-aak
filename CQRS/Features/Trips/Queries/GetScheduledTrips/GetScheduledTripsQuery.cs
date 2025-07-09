using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs.ResponseDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.Trips.Queries.GetScheduledTrips
{
    public class GetScheduledTripsQuery : IRequest<Response<PaginatedResult<TripResponseDto>>>
    {
        public DateTime? CreatedAfter { get; set; } 
        public DateTime? CreatedBefore { get; set; } 
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
