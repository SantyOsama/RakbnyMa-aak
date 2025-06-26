using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.BookTripOrchestrator
{
    public class BookTripCommand : IRequest<Response<int>>
    {
        public string PassengerUserId { get; set; }
        public int TripId { get; set; }
        public int NumberOfSeats { get; set; }
    }

}
