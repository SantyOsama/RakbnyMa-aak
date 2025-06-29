using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.UpdateTrip
{
    public class UpdateTripCommand : IRequest<Response<int>>
    {
        public int TripId { get; set; }
        public TripDto TripDto { get; set; }
    }

}
