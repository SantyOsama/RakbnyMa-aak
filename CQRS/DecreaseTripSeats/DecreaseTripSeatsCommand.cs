using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.DecreaseTripSeats
{
    public class DecreaseTripSeatsCommand : IRequest<Response<string>>
    {
        public int TripId { get; set; }
        public int NumberOfSeats { get; set; }
    }

}
