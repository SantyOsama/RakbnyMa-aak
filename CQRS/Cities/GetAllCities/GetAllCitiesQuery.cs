using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.GetAllCities
{
    public record GetAllCitiesQuery(int Page = 1, int PageSize = 10)
      : IRequest<Response<PaginatedResult<CityDto>>>;


}
