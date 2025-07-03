using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.DecreaseTripSeats
{
    public record DecreaseTripSeatsCommand(DecreaseTripSeatsDto SeatsDto) : IRequest<Response<string>>;

}
