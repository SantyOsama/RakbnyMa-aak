using MediatR;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Queries.GetLastDriverLocation;

namespace RakbnyMa_aak.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrackingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tripId}")]
        public async Task<IActionResult> GetLocation(int tripId)
        {
            var response = await _mediator.Send(new GetLastDriverLocationQuery(tripId));
            return StatusCode(response.StatusCode, response);
        }
    }

}
