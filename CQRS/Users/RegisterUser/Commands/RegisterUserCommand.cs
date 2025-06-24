using MediatR;
using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Users.RegisterUser.Commands
{
    public class RegisterUserCommand : IRequest<Response<string>>
    {
        public RegisterUserDto Dto { get; set; }
        public RegisterUserCommand(RegisterUserDto dto)
        {

            Dto = dto;


        }
    }
}