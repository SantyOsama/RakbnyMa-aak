using MediatR;
using RakbnyMa_aak.CQRS.Commands.UpdateTrip;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateCityInGovernorate;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateDriver;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripBusinessRules;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripIsUpdatable;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripOwner;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.UpdateTrip
{
    public class UpdateTripOrchestratorHandler : IRequestHandler<UpdateTripOrchestrator, Response<int>>
    {
        private readonly IMediator _mediator;

        public UpdateTripOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<int>> Handle(UpdateTripOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.TripDto;

            var isDriver = await _mediator.Send(new ValidateDriverCommand { UserId = request.CurrentUserId });
            if (!isDriver.IsSucceeded)
                return Response<int>.Fail(isDriver.Message);

            var tripResult = await _mediator.Send(new ValidateTripIsUpdatableCommand(request.TripId));
            if (!tripResult.IsSucceeded)
                return Response<int>.Fail(tripResult.Message);

            var trip = tripResult.Data;

          
            var isOwner = await _mediator.Send(new ValidateTripOwnerCommand(request.CurrentUserId, trip.DriverId));
            if (!isOwner.IsSucceeded)
                return Response<int>.Fail(isOwner.Message);

            var validation = await _mediator.Send(new ValidateTripBusinessRulesCommand { Trip = dto });
            if (!validation.IsSucceeded)
                return Response<int>.Fail(validation.Message);

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

            var updateResult = await _mediator.Send(new UpdateTripCommand
            {
                TripId = request.TripId,
                TripDto = dto
            });

            return updateResult;
        }

    }

}
