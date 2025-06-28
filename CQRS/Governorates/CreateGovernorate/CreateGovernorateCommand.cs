using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Governorates.CreateGovernorate
{
    public record CreateGovernorateCommand(GovernorateDto Dto) : IRequest<Response<string>>;
}
