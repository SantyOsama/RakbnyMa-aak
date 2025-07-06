using MediatR;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateCityInGovernorate;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripBusinessRules;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.Trip.Orchestrators.ValidateTripBusinessLogic
{
    public class ValidateTripBusinessLogicOrchestratorHandler : IRequestHandler<ValidateTripBusinessLogicOrchestrator, Response<bool>>
    {
        private readonly IMediator _mediator;

        public ValidateTripBusinessLogicOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(ValidateTripBusinessLogicOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.TripDto;
            // Step 1: Validate trip business rules 
            var validation = await _mediator.Send(new ValidateTripBusinessRulesCommand(dto));
            if (!validation.IsSucceeded)
                return Response<bool>.Fail(validation.Message);

            // Step 2: Ensure the "FromCity" actually belongs to the "FromGovernorate"
            var fromCityValidation = await _mediator.Send(new ValidateCityInGovernorateCommand
            (
                dto.FromCityId,
                dto.FromGovernorateId
            ));
            if (!fromCityValidation.IsSucceeded)
                return Response<bool>.Fail(fromCityValidation.Message);

            // Step 3 Ensure the "ToCity" actually belongs to the "ToGovernorate"
            var toCityValidation = await _mediator.Send(new ValidateCityInGovernorateCommand
            (
                dto.ToCityId,
                dto.ToGovernorateId
            ));
            if (!toCityValidation.IsSucceeded)
                return Response<bool>.Fail(toCityValidation.Message);

            return Response<bool>.Success(true);
        }
    }

}
