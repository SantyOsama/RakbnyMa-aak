using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Trips.CreateTrip;
using RakbnyMa_aak.CQRS.Trips.Delete_Trip;
using RakbnyMa_aak.CQRS.Trips.UpdateTrip;
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
        [Authorize(Roles = "Driver")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrip(int id, [FromBody] UpdateTripDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var command = new UpdateTripCommand { TripDto = dto };
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [Authorize(Roles = "Driver")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var command = new DeleteTripCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
                return BadRequest(result.Message);

            return Ok(result);
        }


    }
}
