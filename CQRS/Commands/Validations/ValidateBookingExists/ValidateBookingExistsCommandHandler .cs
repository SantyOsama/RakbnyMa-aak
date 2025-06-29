using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateBookingExists
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
            if (booking == null)
                return Response<Booking>.Fail("Booking not found.");

            if (booking.IsDeleted)
                return Response<Booking>.Fail("Booking has been deleted.");

            if (booking.RequestStatus == RequestStatus.Cancelled)
                return Response<Booking>.Fail("Booking has been canceled.");

            return Response<Booking>.Success(booking);
        }
    }

}
