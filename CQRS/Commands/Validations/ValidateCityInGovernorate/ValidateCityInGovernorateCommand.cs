using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateCityInGovernorate
{
    public class ValidateCityInGovernorateCommand :IRequest<Response<bool>>
    {
        public int CityId { get; set; }
        public int GovernorateId { get; set; }


    }
}
