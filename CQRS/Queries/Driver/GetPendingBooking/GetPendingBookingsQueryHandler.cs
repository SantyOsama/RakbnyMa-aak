using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.DTOs.DriverDTOs.ResponseDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.CQRS.Queries.Driver.GetPendingBooking
{
    public class GetPendingBookingsQueryHandler : IRequestHandler<GetPendingBookingsQuery, Response<PaginatedResult<BookingStatusResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPendingBookingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<PaginatedResult<BookingStatusResponseDto>>> Handle(GetPendingBookingsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.BookingRepository
                .GetAllQueryable()
                .Where(b => b.RequestStatus == RequestStatus.Pending)
                .Include(b => b.User)
                .Include(b => b.Trip)
                .Select(b => new BookingStatusResponseDto
                {
                    TripID=b.TripId,
                    BookingId = b.Id,
                    UserFullName = b.User.FullName,
                    UserEmail = b.User.Email,
                
                });
           
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = new PaginatedResult<BookingStatusResponseDto>(items, totalCount, request.Page, request.PageSize);

            return Response<PaginatedResult<BookingStatusResponseDto>>.Success(result);
        }
    }
}
