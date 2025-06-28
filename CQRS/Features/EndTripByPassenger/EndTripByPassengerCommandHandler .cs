using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.EndTripByPassenger
{
    public class EndTripByPassengerCommandHandler : IRequestHandler<EndTripByPassengerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EndTripByPassengerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(EndTripByPassengerCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null || booking.IsDeleted || booking.UserId != request.CurrentUserId)
                return Response<bool>.Fail("Unauthorized or booking not found.");

            if (!booking.HasStarted)
                return Response<bool>.Fail("Trip must be started first.");

            booking.HasEnded = true;
            _unitOfWork.BookingRepository.Update(booking);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Trip ended successfully.");
        }
    }

}
