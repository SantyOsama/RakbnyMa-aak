using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Cities.CreateCity
{
    public record CreateCityCommand(CityDto Dto) : IRequest<Response<string>>;
}
