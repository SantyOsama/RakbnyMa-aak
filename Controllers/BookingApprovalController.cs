using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.BookingOrchestrators;

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
            var result = await _mediator.Send(new ApproveBookingRequestCommand(bookingId,currentUserId));
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

        [HttpPost("reject")]
        public async Task<IActionResult> RejectBooking([FromQuery] int bookingId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new RejectBookingRequestCommand(bookingId,currentUserId));
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }
    }

}
