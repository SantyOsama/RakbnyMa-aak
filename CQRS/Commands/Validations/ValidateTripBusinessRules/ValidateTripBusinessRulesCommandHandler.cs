using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripBusinessRules
{

    public class ValidateTripBusinessRulesCommandHandler : IRequestHandler<ValidateTripBusinessRulesCommand, Response<bool>>
    {
        public Task<Response<bool>> Handle(ValidateTripBusinessRulesCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Trip;

            if (dto.AvailableSeats <= 0)
                return Task.FromResult(Response<bool>.Fail("Available seats must be greater than 0."));

            if (dto.TripDate < DateTime.UtcNow.Date)
                return Task.FromResult(Response<bool>.Fail("Trip date must be in the future."));

            return Task.FromResult(Response<bool>.Success(true));
        }
    }

}
