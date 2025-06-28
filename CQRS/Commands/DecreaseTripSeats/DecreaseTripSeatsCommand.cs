using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.DecreaseTripSeats
{
    public class DecreaseTripSeatsCommand : IRequest<Response<string>>
    {
        public DecreaseTripSeatsDto SeatsDto { get; }

        public DecreaseTripSeatsCommand(DecreaseTripSeatsDto seatsDto)
        {
            SeatsDto = seatsDto;
        }
    }

}
