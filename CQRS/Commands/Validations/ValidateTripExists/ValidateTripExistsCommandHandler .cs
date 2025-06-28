using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists
{
    public class ValidateTripExistsCommandHandler : IRequestHandler<ValidateTripExistsCommand, Response<Trip>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateTripExistsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Trip>> Handle(ValidateTripExistsCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);

            if (trip == null)
                return Response<Trip>.Fail("Trip not found.");

            if (trip.IsDeleted)
                return Response<Trip>.Fail("Trip is deleted.");

            if (trip.TripStatus == TripStatus.Cancelled)
                return Response<Trip>.Fail("Trip is canceled.");

            return Response<Trip>.Success(trip);
        }
    }

}
