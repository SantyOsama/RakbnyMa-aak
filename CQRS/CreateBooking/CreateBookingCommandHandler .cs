using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                TripId = request.TripId,
                UserId = request.UserId,
                NumberOfSeats = request.NumberOfSeats,
                BookingDate = DateTime.UtcNow,
                RequestStatus = RequestStatus.Pending
            };

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            return Response<int>.Success(booking.Id, "Trip has been created successfully.");
        }
    }
}
