using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Users.RegisterUser.Commands;
using RakbnyMa_aak.DTOs.UserDTOs;

namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;

        }
        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromForm] RegisterUserDto dto)
        //{
        //    var result = await _mediator.Send(new RegisterUserCommand(dto));
        //    return StatusCode(result.StatusCode, result);
        //}
    }
}