using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.RestoreTripSeats
{
    public class RestoreTripSeatsCommandHandler : IRequestHandler<RestoreTripSeatsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestoreTripSeatsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(RestoreTripSeatsCommand request, CancellationToken cancellationToken)
        {
            //Step 1: Load trip by ID
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);
            if (trip is null)
                return Response<bool>.Fail("Trip not found.");

            //Step 2: Modify
            trip.AvailableSeats += request.SeatsToRestore;
            trip.UpdatedAt = DateTime.UtcNow;

            //Step 3: Save
            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Seats restored successfully.");
        }
    }

}
