using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Repositories.Interfaces;

namespace RakbnyMa_aak.CQRS.Users.RegisterUser.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.RegisterAsync(request.Dto);
        }
    }
}