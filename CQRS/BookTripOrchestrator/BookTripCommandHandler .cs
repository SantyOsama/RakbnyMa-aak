using AutoMapper;
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
        private readonly IMapper _mapper;

        public BookTripCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(BookTripCommand request, CancellationToken cancellationToken)
        {
            var trip = request.TripDetails;

            var isAlreadyBooked = await _mediator.Send(new CheckUserAlreadyBookedCommand(
                new CheckUserAlreadyBookedDto
                {
                    TripId = trip.TripId,
                    UserId = trip.PassengerUserId
                }));

            if (!isAlreadyBooked.IsSucceeded)
                return Response<int>.Fail("Failed to check if already booked.");

            if (isAlreadyBooked.Data)
                return Response<int>.Fail("You have already booked this trip.");

            var decreaseSeatsDto = new DecreaseTripSeatsDto
            {
                TripId = trip.TripId,
                NumberOfSeats = trip.NumberOfSeats
            };

            var seatResult = await _mediator.Send(new DecreaseTripSeatsCommand(decreaseSeatsDto));

            if (!seatResult.IsSucceeded)
                return Response<int>.Fail(seatResult.Message);

            var bookingDto = _mapper.Map<CreateBookingDto>(trip);


            var bookingResult = await _mediator.Send(new CreateBookingCommand(bookingDto));

            if (!bookingResult.IsSucceeded)
                return Response<int>.Fail("Booking failed.");

            return Response<int>.Success(bookingResult.Data, "Trip booked successfully.");
        }

    }
}
