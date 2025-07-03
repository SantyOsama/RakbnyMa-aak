using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Cities.GetCityById
{
    public record GetCityByIdQuery(int Id) : IRequest<Response<CityDto>>;
}
