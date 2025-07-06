using MediatR;
using RakbnyMa_aak.CQRS.Commands.SendNotification;
using RakbnyMa_aak.CQRS.Features.Booking.Commands.UpdateBookingStatus;
using RakbnyMa_aak.CQRS.Features.Booking.Orchestrators.BookValidationOrchestrator;
using RakbnyMa_aak.GeneralResponse;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.CQRS.Features.Booking.Commands.ApproveBookingRequest
{
    public class ApproveBookingOrchestratorHandler : IRequestHandler<ApproveBookingOrchestrator, Response<bool>>
    {
        private readonly IMediator _mediator;

        public ApproveBookingOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(ApproveBookingOrchestrator request, CancellationToken cancellationToken)
        {
            var validation = await _mediator.Send(new BookingValidationOrchestrator(request.BookingId, request.CurrentUserId));
            if (!validation.IsSucceeded)
                return Response<bool>.Fail(validation.Message);

            var result = validation.Data;

            // Step 4: Update booking status to Confirmed (internally checks available seats)
            var approveResult = await _mediator.Send(
                new UpdateBookingStatusCommand(
                    result.BookingId,
                    result.TripId,
                    RequestStatus.Confirmed));
            if (!approveResult.IsSucceeded)
                return Response<bool>.Fail(approveResult.Message);


            await _mediator.Send(new SendNotificationCommand(new SendNotificationDto
            {
                ReceiverId = result.PassengerId,
                SenderUserId = result.DriverId,
                Message = "Your booking request has been approved."
            }));

            return Response<bool>.Success(true, "Booking approved and passenger notified.");
        }
    }
}
