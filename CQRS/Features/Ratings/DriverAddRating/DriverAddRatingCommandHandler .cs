using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.Ratings.DriverAddRating
{
    public class DriverAddRatingCommandHandler : IRequestHandler<DriverAddRatingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DriverAddRatingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(DriverAddRatingCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RatingDto;

            // Step 1: Check trip
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(dto.TripId);
            if (trip == null || trip.IsDeleted)
                return Response<bool>.Fail("Trip not found.");

            // Step 2: Verify the driver owns the trip
            if (trip.DriverId != dto.RaterId)
                return Response<bool>.Fail("You are not authorized to rate passengers on this trip.");

            if (dto.RaterId == dto.RatedPassengerId)
                return Response<bool>.Fail("You cannot rate yourself.");

            // Step 3: Check if the passenger has completed the trip
            var booking = await _unitOfWork.BookingRepository
                .GetConfirmedFinishedBookingQueryable(dto.TripId, dto.RatedPassengerId)
                .FirstOrDefaultAsync();

            if (booking == null)
                return Response<bool>.Fail("This passenger has not completed this trip.");

            // Step 4: Check if already rated
            bool alreadyRated = await _unitOfWork.RatingRepository
                .AnyAsync(r => r.TripId == dto.TripId &&
                               r.RaterId == dto.RaterId &&
                               r.RatedId == dto.RatedPassengerId);

            if (alreadyRated)
                return Response<bool>.Fail("You have already rated this passenger for this trip.");

            // Step 5: Add rating
            var rating = new Rating
            {
                TripId = dto.TripId,
                RaterId = dto.RaterId,
                RatedId = dto.RatedPassengerId,
                RatingValue = dto.RatingValue,
                Comment = dto.Comment,
            };

            await _unitOfWork.RatingRepository.AddAsync(rating);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Passenger rated successfully.");
        }
    }
}
