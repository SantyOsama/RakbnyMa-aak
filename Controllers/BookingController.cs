using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.BookTripOrchestrator;
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
        [HttpPost("book")]
        public async Task<IActionResult> BookTrip([FromBody] BookTripDto dto)
        {
            dto.PassengerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = _mapper.Map<BookTripCommand>(dto);

            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result);
        }


    }
}
