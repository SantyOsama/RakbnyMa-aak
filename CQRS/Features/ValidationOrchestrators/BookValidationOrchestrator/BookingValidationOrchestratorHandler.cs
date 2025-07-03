using MediatR;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateBookingExists;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripOwner;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.BookValidationOrchestrator
{

    public class BookingValidationCommandHandler
    : IRequestHandler<BookingValidationOrchestrator, Response<BookingValidationResultDto>>
    {
        private readonly IMediator _mediator;

        public BookingValidationCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Response<BookingValidationResultDto>> Handle(BookingValidationOrchestrator request, CancellationToken cancellationToken)
        {
            // Step 1: Validate that the booking exists
            var bookingResult = await _mediator.Send(new ValidateBookingExistsCommand(request.BookingId));
            if (!bookingResult.IsSucceeded)
                return Response<BookingValidationResultDto>.Fail(bookingResult.Message);
            var booking = bookingResult.Data;

            // Step 2: Validate that the associated trip exists
            var tripResult = await _mediator.Send(new ValidateTripExistsCommand(booking.TripId));
            if (!tripResult.IsSucceeded)
                return Response<BookingValidationResultDto>.Fail(tripResult.Message);
            var trip = tripResult.Data;

            // Step 3: Validate that the current user is the owner (driver) of the trip
            var ownerResult = await _mediator.Send(new ValidateTripOwnerCommand(request.CurrentUserId, trip.DriverId));
            if (!ownerResult.IsSucceeded)
                return Response<BookingValidationResultDto>.Fail(ownerResult.Message);

            // Step 4: Return the validated data in a DTO
            return Response<BookingValidationResultDto>.Success(new BookingValidationResultDto
            {
                BookingId = booking.Id,
                TripId = trip.TripId,
                PassengerId = booking.UserId,
                DriverId = trip.DriverId
            });
        }
    }

}
