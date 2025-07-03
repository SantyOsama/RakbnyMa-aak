using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Cities.GetCityByName
{
    public record GetCityByNameQuery(string Name) : IRequest<Response<CityDto>>;
}
