using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.StartTripByDriver
{
    public record StartTripByDriverCommand(int TripId, string DriverId) : IRequest<Response<bool>>;
}
