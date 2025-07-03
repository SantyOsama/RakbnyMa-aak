using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Cities.GetCitiesByGovernorateId
{

    public record GetCitiesByGovernorateIdQuery(int GovernorateId) : IRequest<Response<List<CityDto>>>;
}
