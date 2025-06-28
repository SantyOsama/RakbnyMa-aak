using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.UpdateBookingStatus
{
    public class UpdateBookingStatusCommandHandler : IRequestHandler<UpdateBookingStatusCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookingStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(UpdateBookingStatusCommand request, CancellationToken cancellationToken)
        {
            // Get only what's needed from Booking
            var bookingData = await _unitOfWork.BookingRepository.GetAllQueryable()
                .Where(b => b.Id == request.BookingId && !b.IsDeleted)
                .Select(b => new
                {
                    b.NumberOfSeats
                })
                .FirstOrDefaultAsync();

            if (bookingData == null)
                return Response<bool>.Fail("Booking not found.");

            if (request.NewStatus == RequestStatus.Confirmed)
            {
                // Get available seats only
                var availableSeats = await _unitOfWork.TripRepository.GetAllQueryable()
                    .Where(t => t.Id == request.TripId && !t.IsDeleted)
                    .Select(t => t.AvailableSeats)
                    .FirstOrDefaultAsync();

                if (availableSeats == default)
                    return Response<bool>.Fail("Trip not found.");

                if (availableSeats < bookingData.NumberOfSeats)
                    return Response<bool>.Fail("Not enough available seats.");

                // Update Trip (only specific fields)
                var updatedTrip = new Trip
                {
                    Id = request.TripId,
                    AvailableSeats = availableSeats - bookingData.NumberOfSeats,
                    UpdatedAt = DateTime.UtcNow
                };
                _unitOfWork.TripRepository.Update(updatedTrip);
            }

            // Update Booking (only specific fields)
            var updatedBooking = new Booking
            {
                Id = request.BookingId,
                RequestStatus = request.NewStatus,
                UpdatedAt = DateTime.UtcNow
            };
            _unitOfWork.BookingRepository.Update(updatedBooking);

            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, $"Booking status updated to {request.NewStatus}");
        }
    }
}
