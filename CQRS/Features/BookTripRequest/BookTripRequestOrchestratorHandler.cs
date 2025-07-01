using AutoMapper;
using MediatR;
using RakbnyMa_aak.CQRS.Commands.CreateBooking;
using RakbnyMa_aak.CQRS.Commands.SendNotification;
using RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists;
using RakbnyMa_aak.CQRS.Trips.Queries;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Features.BookTripRequest
{
    public class BookTripRequestOrchestratorHandler : IRequestHandler<BookTripRequestOrchestrator, Response<int>>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public BookTripRequestOrchestratorHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(BookTripRequestOrchestrator request, CancellationToken cancellationToken)
        {
            var bookingDto = request.BookingDto;

            var validateTripResponse = await _mediator.Send(new ValidateTripExistsCommand(bookingDto.TripId));
            if (!validateTripResponse.IsSucceeded)
                return Response<int>.Fail(validateTripResponse.Message);

            // Step 1: Create Booking
            var bookingResponse = await _mediator.Send(new CreateBookingCommand(bookingDto));
            if (!bookingResponse.IsSucceeded)
                return bookingResponse;

            // Step 2: Get DriverId
            var driverIdResponse = await _mediator.Send(new GetDriverIdByTripIdQuery(bookingDto.TripId));
            if (!driverIdResponse.IsSucceeded || string.IsNullOrEmpty(driverIdResponse.Data))
                return Response<int>.Fail("Driver not found for this trip.");

            var driverId = driverIdResponse.Data;

            // Step 3: Send Notification to Driver
            await _mediator.Send(new SendNotificationCommand(new SendNotificationDto
            {
                ReceiverId = driverId,
                SenderUserId = bookingDto.UserId,
                Message = "You have a new booking request."
            }));

            return bookingResponse;
        }
    }
}
