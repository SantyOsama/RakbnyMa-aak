using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateBookingForEnding
{
    public class ValidateBookingForEndingCommandHandler : IRequestHandler<ValidateBookingForEndingCommand, Response<Booking>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateBookingForEndingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Booking>> Handle(ValidateBookingForEndingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId);

            if (booking == null || booking.IsDeleted || booking.UserId != request.CurrentUserId)
                return Response<Booking>.Fail("Unauthorized or booking not found.");

            if (!booking.HasStarted)
                return Response<Booking>.Fail("Trip must be started first.");

            return Response<Booking>.Success(booking, "Valid booking for ending");
        }
    }
}
