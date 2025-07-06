using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DriverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterDriver([FromForm] RegisterDriverDto dto)
        //{
        //    try
        //    {
        //        var command = new RegisterDriverCommand(dto);
        //        var result = await _mediator.Send(command);
        //        return Ok(new { message = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}
    }
}
