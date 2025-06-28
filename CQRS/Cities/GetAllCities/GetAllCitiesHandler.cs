using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.GetAllCities
{
    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, Response<List<CityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCitiesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<CityDto>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _unitOfWork.Cities
                .GetAllQueryable()
                .Where(c => !c.IsDeleted)
                .Include(c => c.Governorate)
                .ToListAsync(cancellationToken);

            var cityDtos = _mapper.Map<List<CityDto>>(cities);

            return Response<List<CityDto>>.Success(cityDtos);
        }
    }
}
