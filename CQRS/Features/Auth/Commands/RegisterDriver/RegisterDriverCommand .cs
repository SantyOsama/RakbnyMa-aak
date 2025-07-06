using MediatR;
using RakbnyMa_aak.DTOs.Auth.RequestDTOs;
using RakbnyMa_aak.DTOs.Auth.ResponseDTOs;
using RakbnyMa_aak.GeneralResponse;
using static RakbnyMa_aak.Utilities.Enums;

namespace RakbnyMa_aak.CQRS.Features.Auth.Commands.RegisterDriver
{
    public record RegisterDriverCommand(RegisterDriverRequestDto DriverDto) : IRequest<Response<RegisterResponseDto>>;

}
