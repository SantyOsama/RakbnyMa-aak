using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateTripOwner
{
    public class ValidateTripOwnerCommand : IRequest<Response<bool>>
    {
        public string CurrentUserId { get; set; }
        public string DriverId { get; set; }

        public ValidateTripOwnerCommand(string currentUserId, string driverId)
        {
            CurrentUserId = currentUserId;
            DriverId = driverId;
        }
    }

}
