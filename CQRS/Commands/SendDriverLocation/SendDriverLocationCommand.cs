using MediatR;

namespace RakbnyMa_aak.CQRS.Commands.SendDriverLocation
{
    public class SendDriverLocationCommand : IRequest<bool>
    {
        public int TripId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
