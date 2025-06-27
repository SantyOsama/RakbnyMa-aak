using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Trips.Delete_Trip
{
    public class DeleteTripCommand : IRequest<Response<string>>
    {
        public int TripId { get; set; }

        public DeleteTripCommand(int tripId)
        {
            TripId = tripId;
        }
    }
}
