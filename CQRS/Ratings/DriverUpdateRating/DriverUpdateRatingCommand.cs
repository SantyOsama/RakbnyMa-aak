using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Ratings.DriverUpdateRating
{
    public record DriverUpdateRatingCommand(DriverUpdateRatingDto RatingDto)
        : IRequest<Response<bool>>;
}
