using MediatR;
using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Users.RegisterUser.Commands
{
    public record RegisterUserCommand(RegisterUserDto Dto) : IRequest<Response<string>>;
}