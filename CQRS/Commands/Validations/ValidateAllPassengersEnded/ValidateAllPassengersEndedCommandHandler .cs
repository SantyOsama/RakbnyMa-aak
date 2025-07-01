using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateAllPassengersEnded
{
    public class ValidateAllPassengersEndedCommandHandler : IRequestHandler<ValidateAllPassengersEndedCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateAllPassengersEndedCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(ValidateAllPassengersEndedCommand request, CancellationToken cancellationToken)
        {
            var anyActive = await _unitOfWork.BookingRepository
                .GetBookingsByTripIdQueryable(request.TripId)
                .AnyAsync(b => !b.HasEnded);

            if (anyActive)
                return Response<bool>.Fail("All passengers must end the trip first.", false);

            return Response<bool>.Success(true, "All passengers ended");
        }
    }
}