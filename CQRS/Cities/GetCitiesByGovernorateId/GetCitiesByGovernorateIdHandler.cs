using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.GetCitiesByGovernorateId
{
    public class GetCitiesByGovernorateIdHandler : IRequestHandler<GetCitiesByGovernorateIdQuery, Response<List<CityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCitiesByGovernorateIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<CityDto>>> Handle(GetCitiesByGovernorateIdQuery request, CancellationToken cancellationToken)
        {

            var cities = await _unitOfWork.CityRepository
         .GetAllQueryable()
         .Where(c => c.GovernorateId == request.GovernorateId && !c.IsDeleted)
         .Include(c => c.Governorate)
         .ToListAsync(cancellationToken);

            if (cities == null || !cities.Any())
                return Response<List<CityDto>>.Fail("No cities found for this governorate.");

            var citiesDto = _mapper.Map<List<CityDto>>(cities);
            return Response<List<CityDto>>.Success(citiesDto);
        }
    }
}
