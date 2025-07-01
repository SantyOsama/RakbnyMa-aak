using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Ratings.DriverAddRating
{
    public record DriverAddRatingCommand(DriverAddRatingDto RatingDto) : IRequest<Response<bool>>;
}
