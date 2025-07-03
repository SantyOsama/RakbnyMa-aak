using MediatR;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Drivers.Login.Commands
{
    public record LoginDriverCommand(LoginDto Dto) : IRequest<Response<AuthResponseDto>>;
}
