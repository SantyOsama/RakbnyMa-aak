using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Queries.GetAllTrips
{
    public record GetMyTripsQuery(int Page = 1, int PageSize = 10)
     : IRequest<Response<PaginatedResult<TripDto>>>;

}
