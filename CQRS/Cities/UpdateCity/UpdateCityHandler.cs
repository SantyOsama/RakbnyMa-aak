using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.UpdateCity
{
    public class UpdateCityHandler : IRequestHandler<UpdateCityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.CityRepository.GetByIdAsync(request.Dto.Id);
            if (city == null || city.IsDeleted)
                return Response<string>.Fail("City not found.");

            city.Name = request.Dto.Name;
            city.GovernorateId = request.Dto.GovernorateId;
            city.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.CityRepository.Update(city);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("City updated successfully.");
        }
    }
}
