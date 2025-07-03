using MediatR;
using RakbnyMa_aak.CQRS.Commands.SendNotification;
using RakbnyMa_aak.CQRS.Commands.UpdateBookingStatus;
using RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.BookValidationOrchestrator;
using RakbnyMa_aak.GeneralResponse;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Features.RejectBookingRequest
{
    public class RejectBookingOrchestratorHandler : IRequestHandler<RejectBookingOrchestrator, Response<bool>>
    {
        private readonly IMediator _mediator;

        public RejectBookingOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(RejectBookingOrchestrator request, CancellationToken cancellationToken)
        {
            // Step 1: Use shared BookingValidationOrchestrator
            var validation = await _mediator.Send(new BookingValidationOrchestrator(request.BookingId, request.CurrentUserId));
            if (!validation.IsSucceeded)
                return Response<bool>.Fail(validation.Message);

            var result = validation.Data;

            //Step 2: Update status to Rejected
            var rejectResult = await _mediator.Send(
                new UpdateBookingStatusCommand(
                    result.BookingId,
                    result.TripId,
                    RequestStatus.Rejected));

            if (!rejectResult.IsSucceeded)
                return Response<bool>.Fail(rejectResult.Message);

            //Step 3: Send notification
            await _mediator.Send(new SendNotificationCommand(new SendNotificationDto
            {
                ReceiverId = result.PassengerId,
                SenderUserId = result.DriverId,
                Message = "Your booking request has been rejected."
            }));

            return Response<bool>.Success(true, "Booking rejected and passenger notified.");
        }
    }
}
