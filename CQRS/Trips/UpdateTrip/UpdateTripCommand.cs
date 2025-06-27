using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Trips.UpdateTrip
{
    public class UpdateTripCommand :IRequest<Response<string>>
    {
        public UpdateTripDto TripDto { get; set; }
        public UpdateTripCommand() { }
    }
}
