using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Governorates.UpdateGovernorate
{
    public record UpdateGovernorateCommand(GovernorateDto Dto) : IRequest<Response<string>>;
}
