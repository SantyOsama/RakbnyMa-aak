using MediatR;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.Admin.Login
{
    public record LoginAdminCommand(LoginDto LoginDto) : IRequest<Response<AuthResponseDto>>;

}
