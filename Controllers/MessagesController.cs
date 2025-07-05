using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Features.SendMessage;
using RakbnyMa_aak.CQRS.Queries.GetMessagesByTripId;
using System.Security.Claims;

namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send-group")]
        [Authorize]
        public async Task<IActionResult> SendGroupMessage([FromBody] SendMessageDto dto)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new SendMessageCommand(senderId, dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("trip/{tripId}/messages")]
        [Authorize]
        public async Task<IActionResult> GetMessagesByTripId(int tripId)
        {
            var result = await _mediator.Send(new GetMessagesByTripIdQuery(tripId));
            return Ok(result);
        }

    }
}
