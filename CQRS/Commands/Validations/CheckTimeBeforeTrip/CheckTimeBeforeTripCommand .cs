using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.Validations.CheckTimeBeforeTrip
{
    public class CheckTimeBeforeTripCommand : IRequest<Response<bool>>
    {
        public DateTime StartDateTime { get; set; }
        public int MinimumHours { get; set; } = 3;
    }

}
