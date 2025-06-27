using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.ValidateBookingExists
{
    public class ValidateBookingExistsCommandHandler : IRequestHandler<ValidateBookingExistsCommand, Response<Booking>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateBookingExistsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Booking>> Handle(ValidateBookingExistsCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null || booking.IsDeleted)
                return Response<Booking>.Fail("Booking not found.");

            return Response<Booking>.Success(booking);
        }
    }

}
