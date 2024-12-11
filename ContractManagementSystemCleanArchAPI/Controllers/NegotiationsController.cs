using AutoMapper;
using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.NegotiationInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegotiationsController : ControllerBase
    {
        private readonly INegotiationService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<NegotiationsController> _logger;

        public NegotiationsController(INegotiationService service, IMapper mapper, ILogger<NegotiationsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        // Endpoint to get a negotiator by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNegotiator(int id)
        {
            var negotiation = await _service.GetNegotiatorByIdAsync(id);
            if (negotiation == null)
            {
                return NotFound();
            }
            // var negotiationDto = _mapper.Map<NegotiationDto>(negotiation);
            return Ok(negotiation);
        }

        // Endpoint to add a new negotiator
        [HttpPost]
        public async Task<IActionResult> AddNegotiator([FromForm] NegotiationsDto negotiationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var negotiation = _mapper.Map<Negotiation>(negotiationDto);

            try
            {
                await _service.AddNegotiatorAsync(negotiationDto);
                return Ok("Negotiator added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding negotiator. Negotiator might already exist or there might be another issue.");
                return BadRequest("Error adding Negotiator.");
            }
        }

        // Endpoint to delete a negotiator by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNegotiator(int id)
        {
            var negotiator = await _service.GetNegotiatorByIdAsync(id);
            if (negotiator == null)
            {
                return NotFound();
            }

            await _service.RemoveNegotiatorAsync(negotiator);
            return NoContent();
        }

        // Endpoint to update an existing negotiator
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNegotiator(int id, [FromForm] NegotiationsDto negotiationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var negotiator = await _service.GetNegotiatorByIdAsync(id);
            if (negotiator == null)
            {
                return NotFound();
            }

            // _mapper.Map(negotiationDto, negotiator);

            try
            {
                await _service.UpdateNegotiatorAsync(negotiator);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating negotiator");
                return BadRequest("Error updating negotiator");
            }
        }
    }

}
