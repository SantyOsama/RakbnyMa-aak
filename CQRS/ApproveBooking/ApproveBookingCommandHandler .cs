using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.ApproveBooking
{
    public class ApproveBookingCommandHandler : IRequestHandler<ApproveBookingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproveBookingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(ApproveBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null || booking.IsDeleted)
                return Response<bool>.Fail("Booking not found.");

            var trip = await _unitOfWork.TripRepository.GetByIdAsync(booking.TripId);
            if (trip == null || trip.IsDeleted)
                return Response<bool>.Fail("Trip not found.");

            if (trip.AvailableSeats < booking.NumberOfSeats)
                return Response<bool>.Fail("Not enough available seats.");

            booking.RequestStatus = RequestStatus.Confirmed;
            trip.AvailableSeats -= booking.NumberOfSeats;

            _unitOfWork.BookingRepository.Update(booking);
            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Booking approved and seats reserved.");
        }
    }
}
