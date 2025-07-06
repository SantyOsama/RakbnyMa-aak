using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.DTOs.DriverDTOs;

namespace RakbnyMa_aak.CQRS.Drivers.Queries
{
    public record GetPendingDriversQuery(int Page = 1, int PageSize = 10)
        : IRequest<Response<PaginatedResult<PendingDriverDto>>>;
}
