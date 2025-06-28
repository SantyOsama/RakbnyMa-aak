using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateUserOwnsBooking
{
    public class ValidateUserOwnsBookingCommandHandler : IRequestHandler<ValidateUserOwnsBookingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateUserOwnsBookingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(ValidateUserOwnsBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null || booking.IsDeleted)
                return Response<bool>.Fail("Booking not found.");

            return booking.UserId == request.UserId
                ? Response<bool>.Success(true)
                : Response<bool>.Fail("Unauthorized action.");
        }
    }

}
