using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Governorates.RestoreGovernorate
{
    public record RestoreGovernorateCommand(int Id) : IRequest<Response<string>>;
}
