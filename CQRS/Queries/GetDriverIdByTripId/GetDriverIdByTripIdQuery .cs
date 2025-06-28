using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Trips.Queries
{
    public class GetDriverIdByTripIdQuery : IRequest<Response<string?>>
    {
        public int TripId { get; }

        public GetDriverIdByTripIdQuery(int tripId)
        {
            TripId = tripId;
        }
    }
}
