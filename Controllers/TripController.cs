using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Features.CreateTripOrchestrator;
using RakbnyMa_aak.CQRS.Features.EndTripByDriver;
using RakbnyMa_aak.CQRS.Features.EndTripByPassenger;
using RakbnyMa_aak.CQRS.Features.StartTripByDriver;
using RakbnyMa_aak.CQRS.Features.StartTripByPassenger;
using RakbnyMa_aak.CQRS.Features.UpdateTrip;
using RakbnyMa_aak.CQRS.Queries.GetAllTrips;
using RakbnyMa_aak.CQRS.Trips.Delete_Trip;
using RakbnyMa_aak.CQRS.Trips.GetDriverTripBookings;
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
        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] TripDto dto)
        {
            var command = new CreateTripOrchestrator(dto);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> UpdateTrip(int id, [FromBody] TripDto dto)
        {
          
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new UpdateTripOrchestrator
            {
                TripId = id,
                CurrentUserId = currentUserId,
                TripDto = dto
            });

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new DeleteTripCommand
            {
                TripId = id,
                CurrentUserId = currentUserId
            });

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {
            var result = await _mediator.Send(new GetAllTripsQuery());

            if (!result.IsSucceeded)
                return BadRequest(result);

            return Ok(result);
        }


        [Authorize(Roles = "Driver")]
        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookingsForDriver()
        {
            var driverUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new GetDriverTripBookingsQuery(driverUserId);

            var result = await _mediator.Send(query);

            if (!result.IsSucceeded)
                return BadRequest(result.Message);

            return Ok(result);
        }



        [Authorize(Roles = "Driver")]
        [HttpPost("start-by-driver")]
        public async Task<IActionResult> StartTripByDriver([FromQuery] int tripId)
        {
            var driverId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new StartTripByDriverCommand(tripId, driverId);

            var result = await _mediator.Send(command);

            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }


        [Authorize(Roles = "Driver")]
        [HttpPost("end-by-driver")]
        public async Task<IActionResult> EndTripByDriver([FromQuery] int tripId)
        {
            var driverId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new EndTripByDriverCommand(tripId, driverId);

            var result = await _mediator.Send(command);

            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }


        [Authorize(Roles = "User")]
        [HttpPost("start-by-passenger")]
        public async Task<IActionResult> StartTripByPassenger([FromQuery] int bookingId)
        {
            var passengerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new StartTripByPassengerCommand(bookingId, passengerId);

            var result = await _mediator.Send(command);

            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost("end-by-passenger")]
        public async Task<IActionResult> EndTripByPassenger([FromQuery] int bookingId)
        {
            var passengerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new EndTripByPassengerCommand(bookingId, passengerId);

            var result = await _mediator.Send(command);

            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }


    }
}
