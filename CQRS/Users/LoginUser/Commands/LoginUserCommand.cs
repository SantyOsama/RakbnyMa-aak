using MediatR;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Users.Login.Commands
{
    public record LoginUserCommand(LoginDto Dto) : IRequest<Response<AuthResponseDto>>;
}
