using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using System.Security.Claims;

namespace RakbnyMa_aak.CQRS.Queries.GetAllTrips
{
 

    public class GetMyTripsHandler : IRequestHandler<GetMyTripsQuery, Response<PaginatedResult<TripDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMyTripsHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<PaginatedResult<TripDto>>> Handle(GetMyTripsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Response<PaginatedResult<TripDto>>.Fail("Unauthorized", statusCode: 401);
            }

            var query = _unitOfWork.TripRepository
                .GetAllQueryable()
                .Where(t => t.DriverId == userId && !t.IsDeleted)
                .AsNoTracking();

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedTrips = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var tripDtos = _mapper.Map<List<TripDto>>(pagedTrips);

            var result = new PaginatedResult<TripDto>(
                items: tripDtos,
                totalCount: totalCount,
                page: request.Page,
                pageSize: request.PageSize
            );

            return Response<PaginatedResult<TripDto>>.Success(result);
        }
    }

}
