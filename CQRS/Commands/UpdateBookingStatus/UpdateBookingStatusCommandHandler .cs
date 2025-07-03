using MediatR;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateBookingExists;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Commands.UpdateBookingStatus
{
    public class UpdateBookingStatusCommandHandler : IRequestHandler<UpdateBookingStatusCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public UpdateBookingStatusCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(UpdateBookingStatusCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Validate booking exists using CQRS
            var bookingResult = await _mediator.Send(new ValidateBookingExistsCommand(request.BookingId));
            if (!bookingResult.IsSucceeded)
                return Response<bool>.Fail(bookingResult.Message);

            var booking = bookingResult.Data!; // Now booking is tracked in DbContext

            // Step 2: If confirming, validate trip and available seats
            if (request.NewStatus == RequestStatus.Confirmed)
            {
                var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);
                if (trip == null || trip.IsDeleted)
                    return Response<bool>.Fail("Trip not found.");

                if (trip.AvailableSeats < booking.NumberOfSeats)
                    return Response<bool>.Fail("Not enough available seats.");

                // Update trip details
                trip.AvailableSeats -= booking.NumberOfSeats;
                trip.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.TripRepository.Update(trip);
            }

            //Step 3: Update booking status
            booking.RequestStatus = request.NewStatus;
            booking.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BookingRepository.Update(booking);

            //Step 4: Save changes
            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, $"Booking status updated to {request.NewStatus}");
        }
    }
}
