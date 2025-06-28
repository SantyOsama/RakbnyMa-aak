using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.EndTripByPassenger
{
    public class EndTripByPassengerCommand : IRequest<Response<bool>>
    {
        public int BookingId { get; set; }
        public string CurrentUserId { get; set; }

        public EndTripByPassengerCommand(int bookingId, string currentUserId)
        {
            BookingId = bookingId;
            CurrentUserId = currentUserId;
        }
    }

}
