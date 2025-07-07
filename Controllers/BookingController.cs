using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Features.Booking.Commands.CancelBookingByPassenger;
using RakbnyMa_aak.CQRS.Features.Booking.Commands.UpdateBooking;
using RakbnyMa_aak.CQRS.Features.Booking.Orchestrators.BookTripRequest;
using RakbnyMa_aak.CQRS.Queries.Driver.GetApprovedBookings;
using RakbnyMa_aak.CQRS.Queries.Driver.GetPendingBooking;
using RakbnyMa_aak.DTOs.BookingsDTOs.Requests;
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

        public async Task<IActionResult> BookTrip([FromBody] BookTripDto dto)
        {
            var bookingDto = _mapper.Map<CreateBookingRequestDto>(dto);
            var result = await _mediator.Send(new BookTripRequestOrchestrator(bookingDto));
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "User")]
        [HttpPut("update-booking")]
        public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingDto dto)
        {
            var command = new UpdateBookingOrchestrator(dto);
            var result = await _mediator.Send(command);
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }


        [Authorize(Roles = "User")]
        [HttpDelete("cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking([FromQuery] int bookingId)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(currentUserId))
                return Unauthorized("User ID not found in token.");

            var result = await _mediator.Send(new CancelBookingByPassengerCommand(bookingId, currentUserId));
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
