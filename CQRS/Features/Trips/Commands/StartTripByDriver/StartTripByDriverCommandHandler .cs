using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.CQRS.Features.Trip.Commands.StartTripByDriver
{
    public class StartTripByDriverCommandHandler : IRequestHandler<StartTripByDriverCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StartTripByDriverCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(StartTripByDriverCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);

            if (trip == null || trip.IsDeleted || trip.DriverId != request.DriverId || trip.TripStatus == TripStatus.Cancelled)
                return Response<bool>.Fail("Unauthorized or trip not found.");

            if (trip.TripStatus != TripStatus.Scheduled)
                return Response<bool>.Fail("Trip is already started or completed.");

            if (DateTime.UtcNow < trip.TripDate)
                return Response<bool>.Fail("Cannot start the trip before its scheduled start time.");

            trip.TripStatus = TripStatus.Ongoing;
            trip.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Trip started successfully.");
        }
    }
}
