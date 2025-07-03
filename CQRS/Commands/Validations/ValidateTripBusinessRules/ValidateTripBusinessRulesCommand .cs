using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripBusinessRules
{
    public record ValidateTripBusinessRulesCommand(TripDto Trip) : IRequest<Response<bool>>;
}
