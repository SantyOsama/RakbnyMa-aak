using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Cities.GetCityById
{
    public class GetCityByIdQuery : IRequest<Response<CityDto>>
    {
        public int Id { get; set; }

        public GetCityByIdQuery(int id)
        {
            Id = id;
        }
    }
}
