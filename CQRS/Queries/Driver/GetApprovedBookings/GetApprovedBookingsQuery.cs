using MediatR;
using RakbnyMa_aak.DTOs.DriverDTOs.ResponseDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Queries.Driver.GetApprovedBookings
{
    public record GetApprovedBookingsQuery(int Page = 1, int PageSize = 10)
     : IRequest<Response<PaginatedResult<BookingStatusResponseDto>>>;

}
