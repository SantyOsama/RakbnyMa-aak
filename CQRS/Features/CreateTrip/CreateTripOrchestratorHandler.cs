using MediatR;
using RakbnyMa_aak.CQRS.Commands.PersistTrip;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateCityInGovernorate;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateDriver;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripBusinessRules;
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

            var isDriver = await _mediator.Send(new ValidateDriverCommand { UserId = dto.DriverId });
            if (!isDriver.IsSucceeded) return Response<int>.Fail(isDriver.Message);

            var validation = await _mediator.Send(new ValidateTripBusinessRulesCommand { Trip = dto });
            if (!validation.IsSucceeded) return Response<int>.Fail(validation.Message);
            var fromCityValidation = await _mediator.Send(new ValidateCityInGovernorateCommand
            {
                CityId = dto.FromCityId,
                GovernorateId = dto.FromGovernorateId
            });

            if (!fromCityValidation.IsSucceeded)
                return Response<int>.Fail(fromCityValidation.Message);

            var toCityValidation = await _mediator.Send(new ValidateCityInGovernorateCommand
            {
                CityId = dto.ToCityId,
                GovernorateId = dto.ToGovernorateId
            });

            if (!toCityValidation.IsSucceeded)
                return Response<int>.Fail(toCityValidation.Message);


            var createResult = await _mediator.Send(new PersistTripCommand { TripDto = dto });
            return createResult;
        }
    }

}
