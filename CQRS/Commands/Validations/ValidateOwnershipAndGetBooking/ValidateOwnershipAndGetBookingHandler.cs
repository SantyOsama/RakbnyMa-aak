using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateOwnershipAndGetBooking
{
    public class ValidateOwnershipAndGetBookingHandler : IRequestHandler<ValidateOwnershipAndGetBookingCommand, Response<Booking>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateOwnershipAndGetBookingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Booking>> Handle(ValidateOwnershipAndGetBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null || booking.IsDeleted)
                return Response<Booking>.Fail("Booking not found.");

            if (booking.UserId != request.UserId)
                return Response<Booking>.Fail("Unauthorized action.");

            return Response<Booking>.Success(booking);
        }
    }

}
