using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.RejectBooking
{
    public class RejectBookingCommandHandler : IRequestHandler<RejectBookingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectBookingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(RejectBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null || booking.IsDeleted)
                return Response<bool>.Fail("Booking not found.");

            booking.RequestStatus = RequestStatus.Rejected;

            _unitOfWork.BookingRepository.Update(booking);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Booking rejected.");
        }
    }
}
