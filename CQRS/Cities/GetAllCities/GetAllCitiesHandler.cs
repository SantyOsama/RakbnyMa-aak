using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.GetAllCities
{
    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, Response<PaginatedResult<CityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCitiesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedResult<CityDto>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.CityRepository
                .GetAllQueryable()
                .Where(c => !c.IsDeleted)
                .Include(c => c.Governorate)
                .OrderBy(c => c.Id);

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedItems = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var cityDtos = _mapper.Map<List<CityDto>>(pagedItems);

            var result = new PaginatedResult<CityDto>(
                items: cityDtos,
                totalCount: totalCount,
                page: request.Page,
                pageSize: request.PageSize
            );

            return Response<PaginatedResult<CityDto>>.Success(result);
        }
    }

}
