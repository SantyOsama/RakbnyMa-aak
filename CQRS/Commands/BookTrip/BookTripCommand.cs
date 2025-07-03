using MediatR;
using RakbnyMa_aak.CQRS.Features.BookTripRequest;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.BookTripOrchestrator
{
    public record BookTripCommand(BookTripDto TripDetails) : IRequest<Response<int>>;

}
