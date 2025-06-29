using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Ratings.UserDeleteRating
{
    public class UserDeleteRatingCommandHandler : IRequestHandler<UserDeleteRatingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDeleteRatingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(UserDeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = await _unitOfWork.RatingRepository.GetByIdAsync(request.RatingId);

            if (rating == null)
                return Response<bool>.Fail("Rating not found.");

            if (rating.RaterId != request.RaterId)
                return Response<bool>.Fail("Unauthorized: You can only delete your own ratings.");

            _unitOfWork.RatingRepository.Delete(rating);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Rating deleted successfully.");
        }
    }
}
