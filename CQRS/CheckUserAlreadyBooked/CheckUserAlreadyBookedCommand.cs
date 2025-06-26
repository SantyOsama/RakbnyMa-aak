using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.CheckUserAlreadyBooked
{
    public class CheckUserAlreadyBookedCommand : IRequest<Response<bool>>
    {
        public int TripId { get; set; }
        public string UserId { get; set; }
    }

}
