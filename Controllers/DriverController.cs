using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Drivers.ChangePassword;
using RakbnyMa_aak.CQRS.Drivers.UpdateDriverProfile;
using RakbnyMa_aak.DTOs.DriverDTOs.UpdateProfileDTOs;
using RakbnyMa_aak.GeneralResponse;
using System.Security.Claims;

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


        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _mediator.Send(new ChangePasswordCommand(userId, dto));
                return result.IsSucceeded ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while changing the password.",
                    Error = ex.Message
                });
            }
        }


        [HttpPatch("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateDriverProfileDto dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _mediator.Send(new UpdateDriverProfileCommand(userId, dto));
                return result.IsSucceeded ? Ok(result) : BadRequest(result);
            }
            
            catch (Exception ex)
    {
                return StatusCode(500, Response<string>.Fail($"Unexpected error: {ex.Message}"));
            }
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
