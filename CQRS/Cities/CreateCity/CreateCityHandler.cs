using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.CreateCity
{
    public class CreateCityHandler : IRequestHandler<CreateCityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
          
            var isExist = await _unitOfWork.CityRepository
                .AnyAsync(c => c.Name == request.Dto.Name && c.GovernorateId == request.Dto.GovernorateId);

            if (isExist)
            {
                return Response<string>.Fail("This city already exists in the selected governorate");
            }

            var entity = _mapper.Map<City>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.CityRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("City created");
        }

    }
}
