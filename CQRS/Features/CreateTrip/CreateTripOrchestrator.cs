using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.CreateTripOrchestrator
{
    public class CreateTripOrchestrator : IRequest<Response<int>>
    {
        public TripDto TripDto { get; set; }

        public CreateTripOrchestrator(TripDto dto)
        {
            TripDto = dto;
        }
    }
}