using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Ratings.UserUpdateRating
{
    public record UserUpdateRatingCommand(UserUpdateRatingDto RatingDto) : IRequest<Response<bool>>;
}
