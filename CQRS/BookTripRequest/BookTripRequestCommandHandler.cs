using AutoMapper;
using MediatR;
using RakbnyMa_aak.CQRS.CreateBooking;
using RakbnyMa_aak.CQRS.SendNotificationForDriver;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.BookingOrchestrator
{
    public class BookTripRequestCommandHandler : IRequestHandler<BookTripRequestCommand, Response<int>>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public BookTripRequestCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(BookTripRequestCommand request, CancellationToken cancellationToken)
        {
            var bookingDto = request.BookingDto;

            // Step 1: Create Booking
            var bookingResponse = await _mediator.Send(new CreateBookingCommand(bookingDto));

            if (!bookingResponse.IsSucceeded)
                return bookingResponse;

            // Step 2: Get DriverId (from Trip)
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(bookingDto.TripId);
            if (trip == null)
                return Response<int>.Fail("Trip not found for notification.");

            // Step 3: Send Notification to Driver
            await _mediator.Send(new SendNotificationCommand(new SendNotificationDto
            {
                ReceiverId = trip.DriverId,
                SenderUserId = bookingDto.UserId,
                Message = "You have a new booking request."
            }));

            return bookingResponse;
        }
    }
}
