using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.StartTripByPassenger
{
    public class StartTripByPassengerCommand : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }
        public string CurrentUserId { get; set; }

        public StartTripByPassengerCommand(int bookingId, string currentUserId)
        {
            BookingId = bookingId;
            CurrentUserId = currentUserId;
        }
    }

}
