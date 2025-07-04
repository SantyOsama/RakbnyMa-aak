using AutoMapper;
using MediatR;
using RakbnyMa_aak.CQRS.Commands.CreateBooking;
using RakbnyMa_aak.CQRS.Commands.SendNotification;
using RakbnyMa_aak.CQRS.Commands.Validations.CheckUserAlreadyBooked;
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

            // Step 1: Validate Trip Exists
            var validateTripResponse = await _mediator.Send(new ValidateTripExistsCommand(bookingDto.TripId));
            if (!validateTripResponse.IsSucceeded)
                return Response<int>.Fail(validateTripResponse.Message);

            var trip = validateTripResponse.Data;

            if (bookingDto.NumberOfSeats > trip.AvailableSeats)
                return Response<int>.Fail($"Not enough available seats. Only {trip.AvailableSeats} seats left.");

            // Step 2: Check if user already booked the same trip
            var checkBookingResponse = await _mediator.Send(
                new CheckUserAlreadyBookedCommand(
                    new CheckUserAlreadyBookedDto
                    {
                        UserId = bookingDto.UserId,
                        TripId = bookingDto.TripId
                    }));

            if (!checkBookingResponse.IsSucceeded)
                return Response<int>.Fail(checkBookingResponse.Message);

            Response<int> bookingResponse;

            if (checkBookingResponse.Data) // already booked → can't book again, but can update existing booking
            {
                // Get booking by user and trip
                var existingBooking = await _unitOfWork.BookingRepository
                    .GetBookingByUserAndTripAsync(bookingDto.UserId, bookingDto.TripId);

                if (existingBooking == null)
                    return Response<int>.Fail("Existing booking not found.");

                return Response<int>.Fail("You Can Only Make One Booking, Please try to Update your Booking.");

            }
            else // first time booking → create
            {
                bookingResponse = await _mediator.Send(new CreateBookingCommand(bookingDto));
            }

            if (!bookingResponse.IsSucceeded)
                return bookingResponse;

            // Step 3: Get DriverId by TripId
            var driverIdResponse = await _mediator.Send(new GetDriverIdByTripIdQuery(bookingDto.TripId));
            if (!driverIdResponse.IsSucceeded || string.IsNullOrEmpty(driverIdResponse.Data))
                return Response<int>.Fail("Driver not found for this trip.");

            var driverId = driverIdResponse.Data;

            // Step 4: Send Notification to Driver
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