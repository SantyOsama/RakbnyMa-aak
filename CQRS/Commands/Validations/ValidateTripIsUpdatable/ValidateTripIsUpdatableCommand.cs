using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Models;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripIsUpdatable
{
    public class ValidateTripIsUpdatableCommand : IRequest<Response<Trip>>
    {
        public int TripId { get; set; }

        public ValidateTripIsUpdatableCommand(int tripId)
        {
            TripId = tripId;
        }
    }
}
