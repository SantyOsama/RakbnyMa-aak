using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Commands.RestoreTripSeats
{
    public class RestoreTripSeatsCommand : IRequest<Response<bool>>
    {
        public Trip Trip { get; set; }
        public int SeatsToRestore { get; set; }
    }
}
