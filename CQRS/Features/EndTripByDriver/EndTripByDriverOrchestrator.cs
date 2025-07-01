using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.EndTripByDriver
{
    public class EndTripByDriverOrchestrator : IRequest<Response<bool>>
    {
        public int TripId { get; set; }
        public string DriverId { get; set; }

        public EndTripByDriverOrchestrator(int tripId, string driverId)
        {
            TripId = tripId;
            DriverId = driverId;
        }
    }

}
