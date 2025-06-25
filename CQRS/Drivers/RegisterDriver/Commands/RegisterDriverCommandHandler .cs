using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.Services;
namespace RakbnyMa_aak.CQRS.Drivers.RegisterDriver.Commands
{
    public class RegisterDriverCommandHandler : IRequestHandler<RegisterDriverCommand, Response<string>>
    {
        private readonly IDriverRegistrationService _registrationService;

        public RegisterDriverCommandHandler(IDriverRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public async Task<Response<string>> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
        {
            return await _registrationService.RegisterDriverAsync(request.DriverDto);
        }
    }
}
