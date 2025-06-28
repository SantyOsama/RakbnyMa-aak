using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripExists
{
    public class ValidateTripExistsCommand : IRequest<Response<Trip>>
    {
        public int TripId { get; set; }

        public ValidateTripExistsCommand(int tripId)
        {
            TripId = tripId;
        }
    }

}
