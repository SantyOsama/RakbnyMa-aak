using MediatR;
using RakbnyMa_aak.CQRS.Commands.RestoreTripSeats;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateOwnershipAndGetBooking;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists;
using RakbnyMa_aak.CQRS.Features.ValidationOrchestrators.CancelBookingValidationOrchestrator;
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
            //Step 1: Validate and get info
            var validationResult = await _mediator.Send(new CancelBookingValidationOrchestrator(request.BookingId, request.CurrentUserId));
            if (!validationResult.IsSucceeded)
                return Response<bool>.Fail(validationResult.Message);

            var dto = validationResult.Data;

            //Step 2: Load the booking directly (we only need to update status)
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(dto.BookingId);
            if (booking is null)
                return Response<bool>.Fail("Booking not found.");

            booking.RequestStatus = RequestStatus.Cancelled;
            booking.HasEnded = true;
            booking.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.BookingRepository.Update(booking);

            //Step 3: Restore seats if booking was confirmed // Because only confirmed bookings affect trip seats
            if (dto.WasConfirmed)
            {
                await _mediator.Send(new RestoreTripSeatsCommand(dto.TripId, dto.NumberOfSeats));
            }

            await _unitOfWork.CompleteAsync();

            return Response<bool>.Success(true, "Booking canceled successfully.");
        }

    }

}
