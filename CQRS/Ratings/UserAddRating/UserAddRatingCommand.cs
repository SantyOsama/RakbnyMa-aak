using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Ratings.UserAddRating
{
    public record UserAddRatingCommand(UserAddRatingDto RatingDto) : IRequest<Response<bool>>;
}
