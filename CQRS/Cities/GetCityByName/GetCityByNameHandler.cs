using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.GetCityByName
{
    public class GetCityByNameHandler : IRequestHandler<GetCityByNameQuery, Response<CityDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCityByNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CityDto>> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.CityRepository
                .GetAllQueryable()
                .Include(c => c.Governorate)
                .Where(c => !c.IsDeleted && c.Name.ToLower() == request.Name.ToLower())
                .FirstOrDefaultAsync(cancellationToken);

            if (city == null)
                return Response<CityDto>.Fail("City not found");

            var dto = _mapper.Map<CityDto>(city);
            return Response<CityDto>.Success(dto);
        }
    }
}
