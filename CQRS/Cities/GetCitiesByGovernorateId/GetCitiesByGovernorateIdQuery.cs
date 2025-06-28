using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Cities.GetCitiesByGovernorateId
{

    public class GetCitiesByGovernorateIdQuery : IRequest<Response<List<CityDto>>>
    {
        public int GovernorateId { get; set; }

        public GetCitiesByGovernorateIdQuery(int governorateId)
        {
            GovernorateId = governorateId;
        }
    }
}
