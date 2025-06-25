using MediatR;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Services;

namespace RakbnyMa_aak.CQRS.Users.Login.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<AuthResponseDto>>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<AuthResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginByUserTypeAsync(request.Dto, Enums.Enums.UserType.User);
        }
    }
}
