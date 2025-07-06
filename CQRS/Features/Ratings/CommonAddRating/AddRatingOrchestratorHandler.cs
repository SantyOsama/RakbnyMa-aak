using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.Ratings.CommonAddRating
{
    public class AddRatingOrchestratorHandler : IRequestHandler<AddRatingOrchestrator, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRatingOrchestratorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(AddRatingOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.RatingDto;

            // Step 1: Validate trip
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(dto.TripId);
            if (trip == null || trip.IsDeleted)
                return Response<bool>.Fail("Trip not found.");

            // Step 2: Check access (driver or passenger)
            if (dto.IsDriverRating)
            {
                if (trip.DriverId != dto.RaterId)
                    return Response<bool>.Fail("Driver not authorized for this trip.");

                var booking = await _unitOfWork.BookingRepository
                    .GetConfirmedFinishedBookingQueryable(dto.TripId, dto.RatedId)
                    .FirstOrDefaultAsync();

                if (booking == null)
                    return Response<bool>.Fail("This passenger didn’t complete this trip.");
            }
            else
            {
                var booking = await _unitOfWork.BookingRepository
                    .GetConfirmedFinishedBookingQueryable(dto.TripId, dto.RaterId)
                    .FirstOrDefaultAsync();

                if (booking == null)
                    return Response<bool>.Fail("You can only rate this trip after completing it.");
            }

            // Step 3: Check if already rated
            bool alreadyRated = await _unitOfWork.RatingRepository
                .AnyAsync(r => r.TripId == dto.TripId &&
                               r.RaterId == dto.RaterId &&
                               r.RatedId == dto.RatedId);

            if (alreadyRated)
                return Response<bool>.Fail("You have already rated this person for this trip.");

            // Step 4: Add rating
            var rating = new Rating
            {
                TripId = dto.TripId,
                RaterId = dto.RaterId,
                RatedId = dto.RatedId,
                RatingValue = dto.RatingValue,
                Comment = dto.Comment
            };

            await _unitOfWork.RatingRepository.AddAsync(rating);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Rating added successfully.");
        }
    }

}
