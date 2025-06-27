using MediatR;
using RakbnyMa_aak.CQRS.ApproveBooking;
using RakbnyMa_aak.CQRS.SendNotificationForDriver;
using RakbnyMa_aak.CQRS.ValidateBookingExists;
using RakbnyMa_aak.CQRS.ValidateTripExists;
using RakbnyMa_aak.CQRS.ValidateTripOwner;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.BookingOrchestrators
{
    public class ApproveBookingRequestCommandHandler : IRequestHandler<ApproveBookingRequestCommand, Response<bool>>
    {
        private readonly IMediator _mediator;

        public ApproveBookingRequestCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(ApproveBookingRequestCommand request, CancellationToken cancellationToken)
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

            // Step 4: Approve booking
            var approveResult = await _mediator.Send(new ApproveBookingCommand(request.BookingId));
            if (!approveResult.IsSucceeded)
                return Response<bool>.Fail(approveResult.Message);

            // Step 5: Notify passenger
            await _mediator.Send(new SendNotificationCommand(new SendNotificationDto
            {
                ReceiverId = booking.UserId,
                SenderUserId = trip.DriverId,
                Message = "Your booking request has been approved."
            }));

            return Response<bool>.Success(true, "Booking approved and passenger notified.");
        }
    }
}
