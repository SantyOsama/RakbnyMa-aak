using MediatR;
using RakbnyMa_aak.GeneralResponse;
using RakbnyMa_aak.UOW;

namespace RakbnyMa_aak.CQRS.Commands.Validations.ValidateDriver
{
    public record ValidateDriverCommand(string UserId) : IRequest<Response<bool>>;

}
