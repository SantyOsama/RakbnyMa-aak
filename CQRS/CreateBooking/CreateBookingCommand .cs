using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.CreateBooking
{
    public class CreateBookingCommand : IRequest<Response<int>>
    {
        public int TripId { get; set; }
        public String UserId { get; set; }
        public int NumberOfSeats { get; set; }
    }

}
