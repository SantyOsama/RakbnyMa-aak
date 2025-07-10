using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Features.Auth.Queries.GetUserById;
using RakbnyMa_aak.DTOs.UserDTOs;
using RakbnyMa_aak.GeneralResponse;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<UserResponseDto>>> GetUserById(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery ( id ));

            if (!result.IsSucceeded)
                return NotFound(result);

            return Ok(result);
        }
    }
}