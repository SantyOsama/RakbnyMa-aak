using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripForEnding
{
    public class ValidateTripForEndingCommandHandler : IRequestHandler<ValidateTripForEndingCommand, Response<Trip>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateTripForEndingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Trip>> Handle(ValidateTripForEndingCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);

            if (trip == null || trip.IsDeleted || trip.DriverId != request.DriverId)
                return Response<Trip>.Fail("Unauthorized or trip not found.");

            if (trip.TripStatus != TripStatus.Ongoing)
                return Response<Trip>.Fail("Trip has not started yet.");

            return Response<Trip>.Success(trip, "Valid trip");
        }
    }
}