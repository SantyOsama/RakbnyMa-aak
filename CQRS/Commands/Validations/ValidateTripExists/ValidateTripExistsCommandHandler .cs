using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists
{
    public class ValidateTripExistsCommandHandler : IRequestHandler<ValidateTripExistsCommand, Response<TripValidationResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateTripExistsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<TripValidationResultDto>> Handle(ValidateTripExistsCommand request, CancellationToken cancellationToken)
        {
            var tripDto = await _unitOfWork.TripRepository.GetTripValidationDtoAsync(request.TripId);
            if (tripDto == null)
                return Response<TripValidationResultDto>.Fail("Trip not found.");

            if (tripDto.IsDeleted)
                return Response<TripValidationResultDto>.Fail("Trip is deleted.");

            return Response<TripValidationResultDto>.Success(tripDto);
        }
    }
}
