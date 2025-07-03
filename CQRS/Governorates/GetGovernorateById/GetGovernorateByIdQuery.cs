using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Governorates.GetGovernorateById
{
    public record GetGovernorateByIdQuery(int Id) : IRequest<Response<GovernorateDto>>;
}
