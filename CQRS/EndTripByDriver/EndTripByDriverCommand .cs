using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.EndTripByDriver
{
    public class EndTripByDriverCommand : IRequest<Response<bool>>
    {
        public int TripId { get; set; }
        public string DriverId { get; set; }

        public EndTripByDriverCommand(int tripId, string driverId)
        {
            TripId = tripId;
            DriverId = driverId;
        }
    }

}
