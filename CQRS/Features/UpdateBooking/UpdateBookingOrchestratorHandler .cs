using MediatR;
using Microsoft.EntityFrameworkCore;
using RakbnyMa_aak.CQRS.Commands.DecreaseBookingSeats;
using RakbnyMa_aak.CQRS.Commands.DecreaseTripSeats;
using RakbnyMa_aak.CQRS.Commands.IncreaseBookingSeats;
using RakbnyMa_aak.CQRS.Commands.IncreaseTripSeats;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateOwnershipAndGetBooking;
using RakbnyMa_aak.DTOs;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Features.UpdateBooking
{
    public class UpdateBookingOrchestratorHandler : IRequestHandler<UpdateBookingOrchestrator, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public UpdateBookingOrchestratorHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(UpdateBookingOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.BookingDto;

            // Step 1: Validate ownership
            var ownershipResult = await _mediator.Send(
                new ValidateOwnershipAndGetBookingCommand(dto.BookingId, dto.UserId));

            if (!ownershipResult.IsSucceeded)
                return Response<bool>.Fail(ownershipResult.Message);

            // Step 2: Get current booking details
            var booking = await _unitOfWork.BookingRepository
                .GetAllQueryable()
                .Where(b => b.Id == dto.BookingId)
                .Select(b => new { b.NumberOfSeats, b.RequestStatus })
                .FirstOrDefaultAsync();

            if (booking == null)
                return Response<bool>.Fail("Booking not found.");

            var difference = dto.NewNumberOfSeats - booking.NumberOfSeats;

            // Step 3: No change
            if (difference == 0)
                return Response<bool>.Fail("No change in number of seats.");

            if (booking.RequestStatus == RequestStatus.Confirmed)
            {
                // Booking is already confirmed
                if (difference > 0)
                {
                    // Increase flow
                    var increaseBookingResult = await _mediator.Send(
                        new IncreaseBookingSeatsCommand(dto.BookingId, difference));

                    if (!increaseBookingResult.IsSucceeded)
                        return Response<bool>.Fail(increaseBookingResult.Message);

                    var decreaseTripDto = new DecreaseTripSeatsDto
                    {
                        TripId = dto.TripId,
                        NumberOfSeats = difference
                    };

                    var decreaseTripResult = await _mediator.Send(new DecreaseTripSeatsCommand(decreaseTripDto));
                    if (!decreaseTripResult.IsSucceeded)
                        return Response<bool>.Fail(decreaseTripResult.Message);
                }
                else
                {
                    // Decrease flow
                    var decreaseBookingResult = await _mediator.Send(
                        new DecreaseBookingSeatsCommand(dto.BookingId, -difference));

                    if (!decreaseBookingResult.IsSucceeded)
                        return Response<bool>.Fail(decreaseBookingResult.Message);

                    var increaseTripDto = new IncreaseTripSeatsDto
                    {
                        TripId = dto.TripId,
                        NumberOfSeats = -difference
                    };

                    var increaseTripResult = await _mediator.Send(new IncreaseTripSeatsCommand(increaseTripDto));
                    if (!increaseTripResult.IsSucceeded)
                        return Response<bool>.Fail(increaseTripResult.Message);
                }
            }
            else
            {
                // Booking is not confirmed yet
                if (difference < 0)
                    return Response<bool>.Fail("You cannot decrease seats before approval.");

                var bookingEntity = await _unitOfWork.BookingRepository.GetByIdAsync(dto.BookingId);
                if (bookingEntity == null)
                    return Response<bool>.Fail("Booking not found.");

                bookingEntity.NumberOfSeats += difference;
                bookingEntity.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.BookingRepository.Update(bookingEntity);
                await _unitOfWork.CompleteAsync();
            }

            return Response<bool>.Success(true, "Booking updated successfully.");
        }
    }
}