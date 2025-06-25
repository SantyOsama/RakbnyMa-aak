using MediatR;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Drivers.Login.Commands
{
    public class LoginDriverCommand : IRequest<Response<AuthResponseDto>>
    {
        public LoginDto Dto { get; set; }

        public LoginDriverCommand(LoginDto dto)
        {
            Dto = dto;
        }
    }
}
