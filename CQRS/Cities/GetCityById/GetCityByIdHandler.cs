using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.GetCityById
{
    public class GetCityByIdHandler : IRequestHandler<GetCityByIdQuery, Response<CityDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCityByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CityDto>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.Cities
                .GetAllQueryable()
                .Include(c => c.Governorate)
                .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, cancellationToken);

            if (city == null)
                return Response<CityDto>.Fail("City not found");

            var dto = _mapper.Map<CityDto>(city);
            

            return Response<CityDto>.Success(dto);
        }
    }
 }
