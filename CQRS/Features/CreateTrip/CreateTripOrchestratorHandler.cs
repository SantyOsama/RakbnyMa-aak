using MediatR;
using RakbnyMa_aak.CQRS.Commands.PersistTrip;
using RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.ValidateTripCommon;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.CreateTripOrchestrator
{
    public class CreateTripOrchestratorHandler : IRequestHandler<CreateTripOrchestrator, Response<int>>
    {
        private readonly IMediator _mediator;

        public CreateTripOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<int>> Handle(CreateTripOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.TripDto;

            // Step 1: Validate common validations for the trip DTO
            var commonValidation = await _mediator.Send(new ValidateTripBusinessLogicOrchestrator(request.TripDto));

            if (!commonValidation.IsSucceeded)
                return Response<int>.Fail(commonValidation.Message);

            // Step 2: All validations passed, persist the trip to the database
            var createResult = await _mediator.Send(new PersistTripCommand(dto));

            return createResult;
        }
    }

}
