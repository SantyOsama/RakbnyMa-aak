using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Ratings.CommonAddRating
{
    public record AddRatingOrchestrator(CommonAddRatingDto RatingDto) : IRequest<Response<bool>>;
}
