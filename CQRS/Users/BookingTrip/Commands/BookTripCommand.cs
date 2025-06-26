using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Bookings.Commands
{
    public class BookTripCommand : IRequest<Response<int>>
    {
        public int TripId { get; set; }
        public int NumberOfSeats { get; set; }
        public string PassengerUserId { get; set; } = null!;
        public string PaymentMethod { get; set; } = "Cash";
    }
}
