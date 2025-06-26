using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.ValidateTripBusinessRules
{
    public class ValidateTripBusinessRulesCommand : IRequest<Response<bool>>
    {
        public TripDto Trip { get; set; }
    }
}
