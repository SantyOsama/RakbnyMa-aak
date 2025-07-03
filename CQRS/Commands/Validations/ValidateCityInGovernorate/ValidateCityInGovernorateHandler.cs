using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateCityInGovernorate
{
    public class ValidateCityInGovernorateHandler : IRequestHandler<ValidateCityInGovernorateCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateCityInGovernorateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(ValidateCityInGovernorateCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.CityRepository.GetByIdAsync(request.CityId);

            if (city == null)
                return Response<bool>.Fail("City not found");

            if (city.GovernorateId != request.GovernorateId)
                return Response<bool>.Fail("City does not belong to the selected governorate");

            return Response<bool>.Success(true);
        }
    }
}
