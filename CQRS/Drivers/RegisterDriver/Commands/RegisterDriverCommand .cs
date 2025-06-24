using MediatR;
using RakbnyMa_aak.DTOs.DriverDTOs;
using RakbnyMa_aak.GeneralResponse;
using static RakbnyMa_aak.Enums.Enums;

namespace RakbnyMa_aak.CQRS.Drivers.RegisterDriver.Commands
{
    public record RegisterDriverCommand(RegisterDriverDto DriverDto) : IRequest<Response<string>>;

}
