using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Ratings.DriverDeleteRating
{
    public record DriverDeleteRatingCommand(int RatingId, string RaterId) : IRequest<Response<bool>>;
}
