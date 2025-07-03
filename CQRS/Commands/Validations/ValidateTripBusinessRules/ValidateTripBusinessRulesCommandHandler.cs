using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripBusinessRules
{
    public class ValidateTripBusinessRulesCommandHandler : IRequestHandler<ValidateTripBusinessRulesCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateTripBusinessRulesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(ValidateTripBusinessRulesCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Trip;

            if (dto.AvailableSeats <= 0)
                return Response<bool>.Fail("Available seats must be greater than 0.");

            if (dto.TripDate < DateTime.UtcNow.Date)
                return Response<bool>.Fail("Trip date must be in the future.");

            var carSeats = await _unitOfWork.DriverRepository
                    .GetAllQueryable()  
                    .Where(d => d.UserId == dto.DriverId)
                    .Select(d => d.CarCapacity)
                    .FirstOrDefaultAsync();

            if (carSeats == 0)
                return Response<bool>.Fail("Driver not found.");

            if (dto.AvailableSeats > carSeats)
                return Response<bool>.Fail($"Available seats cannot exceed the car seat count ({carSeats}).");

            return Response<bool>.Success(true);
        }
    }
}
