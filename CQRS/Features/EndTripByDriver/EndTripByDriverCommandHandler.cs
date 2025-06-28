using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Features.EndTripByDriver
{
    public class EndTripByDriverCommandHandler : IRequestHandler<EndTripByDriverCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EndTripByDriverCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(EndTripByDriverCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);
            if (trip == null || trip.IsDeleted || trip.DriverId != request.DriverId)
                return Response<bool>.Fail("Unauthorized or trip not found.");

            if (!(trip.TripStatus == TripStatus.Ongoing))
                return Response<bool>.Fail("Trip has not started yet.");

            var bookings = await _unitOfWork.BookingRepository
                .GetBookingsByTripIdQueryable(request.TripId)
                .ToListAsync();

            if (bookings.Any(b => !b.HasEnded))
                return Response<bool>.Fail("All passengers must end the trip first.");

            trip.TripStatus = TripStatus.Completed;
            trip.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Trip ended successfully.");
        }
    }

}
