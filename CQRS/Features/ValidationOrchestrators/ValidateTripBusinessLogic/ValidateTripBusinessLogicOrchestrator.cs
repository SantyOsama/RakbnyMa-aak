using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.ValidateTripCommon
{
    public record ValidateTripBusinessLogicOrchestrator(TripDto TripDto) : IRequest<Response<bool>>;

}
