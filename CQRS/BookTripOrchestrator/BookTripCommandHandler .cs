using MediatR;
using RakbnyMa_aak.CQRS.CheckUserAlreadyBooked;
using RakbnyMa_aak.CQRS.CreateBooking;
using RakbnyMa_aak.CQRS.DecreaseTripSeats;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.BookTripOrchestrator
{
    public class BookTripCommandHandler : IRequestHandler<BookTripCommand, Response<int>>
    {
        private readonly IMediator _mediator;

        public BookTripCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<int>> Handle(BookTripCommand request, CancellationToken cancellationToken)
        {
            var isAlreadyBooked = await _mediator.Send(new CheckUserAlreadyBookedCommand
            {
                TripId = request.TripId,
                UserId = request.PassengerUserId
            });

            if (!isAlreadyBooked.IsSucceeded)
                return Response<int>.Fail("Failed to check if already booked.");

            if (isAlreadyBooked.Data)
                return Response<int>.Fail("You have already booked this trip.");

            var seatResult = await _mediator.Send(new DecreaseTripSeatsCommand
            {
                TripId = request.TripId,
                NumberOfSeats = request.NumberOfSeats
            });

            if (!seatResult.IsSucceeded)
                return Response<int>.Fail(seatResult.Message);

            var bookingResult = await _mediator.Send(new CreateBookingCommand
            {
                TripId = request.TripId,
                UserId = request.PassengerUserId,
                NumberOfSeats = request.NumberOfSeats
            });

            if (!bookingResult.IsSucceeded)
                return Response<int>.Fail("Booking failed.");

            return Response<int>.Success(bookingResult.Data, "Trip booked successfully.");
        }
    }
}
