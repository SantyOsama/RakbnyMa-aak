using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakbnyMa_aak.CQRS.Cities;
using RakbnyMa_aak.CQRS.Cities.CreateCity;
using RakbnyMa_aak.CQRS.Cities.DeleteCity;
using RakbnyMa_aak.CQRS.Cities.GetAllCities;
using RakbnyMa_aak.CQRS.Cities.GetCitiesByGovernorateId;
using RakbnyMa_aak.CQRS.Cities.GetCityById;
using RakbnyMa_aak.CQRS.Cities.UpdateCity;
using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCitiesQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityDto dto)
        {
            var result = await _mediator.Send(new CreateCityCommand(dto));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CityDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var result = await _mediator.Send(new UpdateCityCommand(dto));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCityCommand(id));
            return Ok(result);
        }

        [HttpGet("by-governorate/{governorateId}")]
        public async Task<ActionResult<Response<List<CityDto>>>> GetCitiesByGovernorateId(int governorateId)
        {
            var result = await _mediator.Send(new GetCitiesByGovernorateIdQuery(governorateId));

            if (!result.IsSucceeded)
                return NotFound(result);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<CityDto>>> GetCityById(int id)
        {
            var result = await _mediator.Send(new GetCityByIdQuery(id));

            if (!result.IsSucceeded)
                return NotFound(result);

            return Ok(result);
        }
    }
}
