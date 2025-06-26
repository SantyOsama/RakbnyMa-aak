using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Trips.CreateTrip;
using RakbnyMa_aak.DTOs.TripDTOs;
using System.Security.Claims;

namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TripController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Driver")]

        [HttpPost("create")]
        public async Task<IActionResult> CreateTrip([FromBody] TripDto dto)
        {
            var command = new CreateTripCommand(dto);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
