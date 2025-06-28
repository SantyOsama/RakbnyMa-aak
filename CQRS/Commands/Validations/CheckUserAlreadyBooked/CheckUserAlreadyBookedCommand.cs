using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.Validations.CheckUserAlreadyBooked
{
    public class CheckUserAlreadyBookedCommand : IRequest<Response<bool>>
    {
        public CheckUserAlreadyBookedDto checkUserAlreadyBookedDto { get; set; }
        public CheckUserAlreadyBookedCommand(CheckUserAlreadyBookedDto checkUserAlreadyBookedDto)
        {
            this.checkUserAlreadyBookedDto = checkUserAlreadyBookedDto;
        }
    }

}
