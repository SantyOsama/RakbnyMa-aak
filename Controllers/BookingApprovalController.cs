using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Features.Booking.Commands.ApproveBookingRequest;
using RakbnyMa_aak.CQRS.Features.Booking.Commands.RejectBookingRequest;
using RakbnyMa_aak.CQRS.Queries.Driver.GetPendingBooking;
using System.Security.Claims;
namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Driver")]
    public class BookingApprovalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingApprovalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveBooking([FromQuery] int bookingId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new ApproveBookingOrchestrator(bookingId,currentUserId));
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }
       


        [HttpPost("reject")]
        public async Task<IActionResult> RejectBooking([FromQuery] int bookingId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new RejectBookingOrchestrator(bookingId,currentUserId));
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }
    }

}
