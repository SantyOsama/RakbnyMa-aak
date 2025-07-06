using MediatR;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.CQRS.Features.Admin.ApproveDriver
{
    public record ApproveDriverCommand(int DriverId) : IRequest<Response<bool>>;
}
