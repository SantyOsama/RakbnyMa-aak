using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.CQRS.Drivers.Queries;
using RakbnyMa_aak.DTOs.DriverDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Drivers.Handlers
{
    public class GetPendingDriversQueryHandler : IRequestHandler<GetPendingDriversQuery, Response<PaginatedResult<PendingDriverDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPendingDriversQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<PaginatedResult<PendingDriverDto>>> Handle(GetPendingDriversQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.DriverRepository
                .GetAllQueryable()
                .Where(d => !d.IsApproved)
                .Include(d => d.User)
                .Select(d => new PendingDriverDto
                {
                    Id = d.Id,
                    FullName = d.User.FullName,
                    Email = d.User.Email,
                    PhoneNumber = d.User.PhoneNumber,
                    NationalId = d.NationalId,
                    CarModel = d.CarModel,
                    CarType = d.CarType,
                    IsApproved = d.IsApproved
                });

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = new PaginatedResult<PendingDriverDto>(items, totalCount, request.Page, request.PageSize);

            return Response<PaginatedResult<PendingDriverDto>>.Success(result);
        }
    }
}
