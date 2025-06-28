using AutoMapper;
using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Cities.GetAllCities
{
    public record GetAllCitiesQuery : IRequest<Response<List<CityDto>>>;
   
}
