using MediatR;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Drivers.Login.Commands;
using RakbnyMa_aak.CQRS.Drivers.RegisterDriver.Commands;
using RakbnyMa_aak.CQRS.Features.Admin.Login;
using RakbnyMa_aak.CQRS.Users.Login.Commands;
using RakbnyMa_aak.CQRS.Users.RegisterUser.Commands;
using RakbnyMa_aak.DTOs.Auth;
using RakbnyMa_aak.DTOs.DriverDTOs;
using RakbnyMa_aak.DTOs.UserDTOs;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator) => _mediator = mediator;


    [HttpPost("admin/login")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginDto dto)
    {
        var result = await _mediator.Send(new LoginAdminCommand(dto));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("user/register")]
    public async Task<IActionResult> RegisterUser([FromForm] RegisterUserDto dto)
    {
        var result = await _mediator.Send(new RegisterUserCommand(dto));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("driver/register")]
    public async Task<IActionResult> RegisterDriver([FromForm] RegisterDriverDto dto)
    {
        var result = await _mediator.Send(new RegisterDriverCommand(dto));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("user/login")]
    public async Task<IActionResult> LoginUser(LoginDto dto)
    {
        var result = await _mediator.Send(new LoginUserCommand(dto));
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }

    [HttpPost("driver/login")]
    public async Task<IActionResult> LoginDriver(LoginDto dto)
    {
        var result = await _mediator.Send(new LoginDriverCommand(dto));
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
}
