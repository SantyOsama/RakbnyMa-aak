using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Governorates.GetGovernorateByName
{
    public record GetGovernorateByNameQuery(string Name) : IRequest<Response<GovernorateDto>>;
}
