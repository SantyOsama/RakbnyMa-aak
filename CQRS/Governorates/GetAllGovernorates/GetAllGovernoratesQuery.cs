using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.CQRS.Governorates; 


namespace RakbnyMa_aak.CQRS.Governorates.GetAllGovernorates
{
    public record GetAllGovernoratesQuery(int Page = 1, int PageSize = 10) : IRequest<Response<PaginatedResult<GovernorateDto>>>;

}
