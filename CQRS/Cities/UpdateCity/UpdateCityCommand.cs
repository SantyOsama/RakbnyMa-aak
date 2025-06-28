using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Cities.UpdateCity
{
    public record UpdateCityCommand(CityDto Dto) : IRequest<Response<string>>;
}
