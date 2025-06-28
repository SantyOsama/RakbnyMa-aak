using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Governorates.GetAllGovernorates
{
    public record GetAllGovernoratesQuery : IRequest<Response<List<GovernorateDto>>>;
}
