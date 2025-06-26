using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Trips.CreateTrip.Command
{
    public class CreateTripCommand : IRequest<Response<int>>
    {
        public TripDto TripDto { get; set; }

        public CreateTripCommand(TripDto dto)
        {
            TripDto = dto;
        }
    }
}
