using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Ratings.UserDeleteRating
{
    public record UserDeleteRatingCommand(int RatingId, string RaterId) : IRequest<Response<bool>>;
}
