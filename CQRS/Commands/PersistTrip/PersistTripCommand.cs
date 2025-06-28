using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.PersistTrip
{
    public class PersistTripCommand : IRequest<Response<int>>
    {
        public TripDto TripDto { get; set; }
    }
}
