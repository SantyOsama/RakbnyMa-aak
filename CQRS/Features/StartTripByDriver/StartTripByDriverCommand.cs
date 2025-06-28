using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.StartTripByDriver
{
    public class StartTripByDriverCommand : IRequest<Response<bool>>
    {
        public int TripId { get; set; }
        public string DriverId { get; set; }

        public StartTripByDriverCommand(int tripId, string driverId)
        {
            TripId = tripId;
            DriverId = driverId;
        }
    }
}
