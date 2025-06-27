using MediatR;
using RakbnyMa_aak.CQRS.RejectBooking;
using RakbnyMa_aak.CQRS.SendNotificationForDriver;
using RakbnyMa_aak.CQRS.ValidateBookingExists;
using RakbnyMa_aak.CQRS.ValidateTripExists;
using RakbnyMa_aak.CQRS.ValidateTripOwner;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.BookingOrchestrators
{
    public class RejectBookingRequestCommandHandler : IRequestHandler<RejectBookingRequestCommand, Response<bool>>
    {
        private readonly IMediator _mediator;

        public RejectBookingRequestCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(RejectBookingRequestCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Validate booking exists
            var bookingResult = await _mediator.Send(new ValidateBookingExistsCommand(request.BookingId));
            if (!bookingResult.IsSucceeded)
                return Response<bool>.Fail(bookingResult.Message);
            Booking booking = bookingResult.Data;

            // Step 2: Validate trip exists
            var tripResult = await _mediator.Send(new ValidateTripExistsCommand(booking.TripId));
            if (!tripResult.IsSucceeded)
                return Response<bool>.Fail(tripResult.Message);
            Trip trip = tripResult.Data;

            // Step 3: Check if current user is the trip owner
            var ownerResult = await _mediator.Send(new ValidateTripOwnerCommand(request.CurrentUserId, trip.DriverId));
            if (!ownerResult.IsSucceeded)
                return Response<bool>.Fail(ownerResult.Message);

            // Step 4: Reject booking
            var rejectResult = await _mediator.Send(new RejectBookingCommand(request.BookingId));
            if (!rejectResult.IsSucceeded)
                return Response<bool>.Fail(rejectResult.Message);

            // Step 5: Send notification to passenger
            await _mediator.Send(new SendNotificationCommand(new SendNotificationDto
            {
                ReceiverId = booking.UserId,
                SenderUserId = booking.Trip.DriverId,
                Message = "Your booking request has been rejected."
            }));

            return Response<bool>.Success(true, "Booking rejected and passenger notified.");
        }
    }
}
