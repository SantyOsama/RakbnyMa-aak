using MediatR;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Services;

namespace RakbnyMa_aak.CQRS.Drivers.Login.Commands
{
    public class LoginDriverCommandHandler : IRequestHandler<LoginDriverCommand, Response<AuthResponseDto>>
    {
        private readonly IAuthService _authService;

        public LoginDriverCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<AuthResponseDto>> Handle(LoginDriverCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginByUserTypeAsync(request.Dto, Enums.Enums.UserType.Driver);
        }
    }
}
