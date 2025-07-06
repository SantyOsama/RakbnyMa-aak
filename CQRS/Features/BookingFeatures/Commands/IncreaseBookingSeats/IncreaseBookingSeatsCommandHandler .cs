using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.Booking.Commands.IncreaseBookingSeats
{
    public class IncreaseBookingSeatsCommandHandler : IRequestHandler<IncreaseBookingSeatsCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncreaseBookingSeatsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(IncreaseBookingSeatsCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null || booking.IsDeleted)
                return Response<int>.Fail("Booking not found.");

            // Update trip and booking
            booking.NumberOfSeats += request.SeatsToAdd;

            booking.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BookingRepository.Update(booking);

            await _unitOfWork.CompleteAsync();

            return Response<int>.Success(booking.Id, "Booking seats increased successfully.");
        }
    }
}