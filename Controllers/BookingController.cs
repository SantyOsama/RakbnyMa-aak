using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.BookingOrchestrator;
using RakbnyMa_aak.CQRS.BookTripOrchestrator;
using RakbnyMa_aak.CQRS.CreateBooking;
namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookingController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookTrip([FromBody] BookTripDto dto)
        {
            var bookingDto = _mapper.Map<CreateBookingDto>(dto);
            var result = await _mediator.Send(new BookTripRequestCommand(bookingDto));
            return result.IsSucceeded ? Ok(result) : BadRequest(result);
        }

    }
}
