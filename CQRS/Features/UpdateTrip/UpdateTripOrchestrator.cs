using MediatR;
using RakbnyMa_aak.DTOs.TripDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.UpdateTrip
{
    public class UpdateTripOrchestrator : IRequest<Response<int>>
    {
        public int TripId { get; set; }
        public TripDto TripDto { get; set; }
        public string CurrentUserId { get; set; }

        public UpdateTripOrchestrator(int tripId, TripDto dto, string userId)
        {
            TripId = tripId;
            TripDto = dto;
            CurrentUserId = userId;
        }
        public UpdateTripOrchestrator() { }
    }

}
