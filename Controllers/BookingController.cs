using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Features.BookingFeatures.Commands.CancelBookingByPassenger;
using RakbnyMa_aak.CQRS.Features.BookingFeatures.Orchestrators.BookTripRequest;
using RakbnyMa_aak.CQRS.Features.BookingFeatures.Orchestrators.UpdateBookingOrchestrator;
using RakbnyMa_aak.CQRS.Queries.Driver.GetApprovedBookings;
using RakbnyMa_aak.CQRS.Queries.Driver.GetPendingBooking;
using RakbnyMa_aak.DTOs.BookingsDTOs.Requests;
using RakbnyMa_aak.DTOs.BookingsDTOs.RequestsDTOs;
using System.Security.Claims;
namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookingController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpPost("book")]
        public async Task<IActionResult> BookTrip([FromBody] BookTripRequestDto dto)
        {
            var result = await _mediator.Send(new BookTripRequestOrchestrator(dto));
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "User")]
        [HttpPut("update-booking")]
        public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingRequestDto dto)
        {
            var command = new UpdateBookingOrchestrator(dto);
            var result = await _mediator.Send(command);
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }


        [Authorize(Roles = "User")]
        [HttpDelete("cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking([FromBody] HandleBookingRequestDto dto)
        {
            var result = await _mediator.Send(new CancelBookingByPassengerCommand(dto));
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }
        [Authorize(Roles = "Driver")]
        [HttpGet("Pending")]
        public async Task<IActionResult> GetPendingBookings([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPendingBookingsQuery(page, pageSize));
            return Ok(result);
        }

        [Authorize(Roles = "Driver")]
        [HttpGet("Confirmed")]
        public async Task<IActionResult> GetApprovedBookings([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetApprovedBookingsQuery(page, pageSize));
            return Ok(result);
        }



    }
}
