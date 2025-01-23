using FusionAPI_Framework.Interfaces;
using FusionAPI_Framework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FusionAPI_Framework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabourController : ControllerBase
    {
        private readonly ILabourService _labourService;

        public LabourController(ILabourService labourService)
        {
            _labourService = labourService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Labour>>> GetAllLabours()
        {
            return Ok(await _labourService.GetAllLabour());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Labour>> GetLabour(int id)
        {
            var labour = await _labourService.GetLabourId(id);
            if (labour == null)
            {
                return NotFound();
            }
            return Ok(labour);
        }

        [HttpPost]
        public async Task<ActionResult> AddLabour(Labour labour)
        {
            await _labourService.AddLabour(labour);
            return CreatedAtAction(nameof(GetLabour), new { id = labour.Id }, labour);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLabour(int id, Labour labour)
        {
            if (id != labour.Id)
            {
                return BadRequest();
            }

            await _labourService.UpdateLabour(labour);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabour(int id)
        {
            await _labourService.DeleteLabour(id);
            return NoContent();
        }
    }
}
