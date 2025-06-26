using MediatR;
using RakbnyMa_aak.CQRS.PersistTrip;
using RakbnyMa_aak.CQRS.Trips.CreateTrip.Command;
using RakbnyMa_aak.CQRS.ValidateDriver;
using RakbnyMa_aak.CQRS.ValidateTripBusinessRules;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Orchestrator
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, Response<int>>
    {
        private readonly IMediator _mediator;

        public CreateTripCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<int>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            var dto = request.TripDto;

            var isDriver = await _mediator.Send(new ValidateDriverCommand { UserId = dto.DriverId });
            if (!isDriver.IsSucceeded) return Response<int>.Fail(isDriver.Message);

            var validation = await _mediator.Send(new ValidateTripBusinessRulesCommand { Trip = dto });
            if (!validation.IsSucceeded) return Response<int>.Fail(validation.Message);

            var createResult = await _mediator.Send(new PersistTripCommand { TripDto = dto });
            return createResult;
        }
    }

}
