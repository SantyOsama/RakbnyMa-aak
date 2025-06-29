using MediatR;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Ratings.UserAddRating;
using RakbnyMa_aak.CQRS.Ratings.UserUpdateRating;
using RakbnyMa_aak.CQRS.Ratings.UserDeleteRating;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RatingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// Add rating by passenger after trip ends
        [HttpPost("add")]
        public async Task<IActionResult> AddRating([FromBody] UserAddRatingDto dto)
        {
            var result = await _mediator.Send(new UserAddRatingCommand(dto));
            return StatusCode(result.StatusCode, result);
        }

        /// Update existing rating by rater
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRating([FromBody] UserUpdateRatingDto dto)
        {
            var result = await _mediator.Send(new UserUpdateRatingCommand(dto));
            return StatusCode(result.StatusCode, result);
        }

        /// Delete existing rating
        [HttpDelete("delete/{ratingId}")]
        public async Task<IActionResult> DeleteRating([FromRoute] int ratingId, [FromQuery] string raterId)
        {
            var result = await _mediator.Send(new UserDeleteRatingCommand(ratingId, raterId));
            return StatusCode(result.StatusCode, result);
        }
    }
}
