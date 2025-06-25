using MediatR;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Users.Login.Commands
{
    public class LoginUserCommand : IRequest<Response<AuthResponseDto>>
    {
        public LoginDto Dto { get; set; }

        public LoginUserCommand(LoginDto dto)
        {
            Dto = dto;
        }
    }
}
