using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Services;
using RakbnyMa_aak.Services.Users;

namespace RakbnyMa_aak.CQRS.Users.RegisterUser.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
    {
        private readonly IUserRegistrationService _registrationService;

        public RegisterUserCommandHandler(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _registrationService.RegisterUserAsync(request.Dto);
        }
    }
}
