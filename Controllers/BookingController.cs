using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.BookTripOrchestrator;
using RakbnyMa_aak.GeneralResponse;
using System.Security.Claims;

namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookingController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //[HttpPost("book")]
        //public async Task<IActionResult> BookTrip([FromBody] BookTripDto command)
        //{
        //    command.PassengerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var result = await _mediator.Send(command);

        //    if (!result.IsSucceeded)
        //        return BadRequest(result);

        //    return Ok(result);
        //}
        [Authorize]
        [HttpPost("book")]
        public async Task<IActionResult> BookTrip([FromBody] BookTripDto dto)
        {
            // Extract user ID from JWT claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(Response<string>.Fail("User not authorized."));

            // Inject user ID into DTO
            dto.UserId = userId;

            // Directly create command (no AutoMapper needed)
            var command = new BookTripCommand(dto);

            var result = await _mediator.Send(command);

            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }


    }
}
