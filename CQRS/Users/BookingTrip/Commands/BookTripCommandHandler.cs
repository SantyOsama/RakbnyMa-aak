using MediatR;
using RakbnyMa_aak.CQRS.Bookings.Commands;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;
using RakbnyMa_aak.UOW;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Users.BookingTrip.Commands
{
    public class BookTripCommandHandler : IRequestHandler<BookTripCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookTripCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(BookTripCommand request, CancellationToken cancellationToken)
        {
            //var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);
            //if (trip is null || trip.IsDeleted || !trip.IsRecruiting)
            //    return Response<int>.Fail("Trip is unavailable or not found.");

            //if (trip.AvailableSeats < request.NumberOfSeats)
            //    return Response<int>.Fail("Not enough available seats.");

            var isAlreadyBooked = await _unitOfWork.BookingRepository
                .IsUserAlreadyBookedAsync(request.PassengerUserId, request.TripId);

            if (isAlreadyBooked)
                return Response<int>.Fail("You have already booked this trip.");

            var booking = new Booking
            {
                TripId = request.TripId,
                UserId = request.PassengerUserId,
                NumberOfSeats = request.NumberOfSeats,
                BookingDate = DateTime.UtcNow,
                RequestStatus = RequestStatus.Pending,
               // IsPaid = request.PaymentMethod.ToLower() == "online" ? false : true
            };

            await _unitOfWork.BookingRepository.AddAsync(booking);

            //trip.AvailableSeats -= request.NumberOfSeats;
           // _unitOfWork.TripRepository.Update(trip);

            await _unitOfWork.CompleteAsync();

            return Response<int>.Success(booking.Id, "Trip booked successfully.");
        }
    }

}
