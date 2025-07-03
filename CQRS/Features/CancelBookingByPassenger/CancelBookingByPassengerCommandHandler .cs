using MediatR;
using RakbnyMa_aak.CQRS.Commands.RestoreTripSeats;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateOwnershipAndGetBooking;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Features.CancelBookingByPassenger
{
    public class CancelBookingByPassengerHandler : IRequestHandler<CancelBookingByPassengerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CancelBookingByPassengerHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(CancelBookingByPassengerCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Validate ownership & get booking
            var ownershipResult = await _mediator.Send(new ValidateOwnershipAndGetBookingCommand(request.BookingId, request.CurrentUserId));
            if (!ownershipResult.IsSucceeded)
                return Response<bool>.Fail(ownershipResult.Message);

            var booking = ownershipResult.Data!;

            // Step 2: Validate Trip
            var tripResult = await _mediator.Send(new ValidateTripExistsCommand(booking.TripId));
            if (!tripResult.IsSucceeded)
                return Response<bool>.Fail(tripResult.Message);

            var trip = tripResult.Data;

            // Step 3: Check cancellation window
            var now = DateTime.UtcNow;
            if (trip.TripDate <= now.AddHours(3))
                return Response<bool>.Fail("You can only cancel a booking at least 3 hours before the trip starts.");

            // Step 4: Save old status BEFORE modifying
            bool wasConfirmed = booking.RequestStatus == RequestStatus.Confirmed;

            // Step 5: Mark booking as cancelled
            booking.RequestStatus = RequestStatus.Cancelled;
            booking.HasEnded = true;
            booking.UpdatedAt = now;
            _unitOfWork.BookingRepository.Update(booking);

            // Step 6: If the booking was confirmed, restore seats
            if (wasConfirmed)
            {
                await _mediator.Send(new RestoreTripSeatsCommand(trip, booking.NumberOfSeats));

            }

            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Booking canceled successfully.");
        }
    }

}
