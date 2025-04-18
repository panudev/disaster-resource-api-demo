using Microsoft.AspNetCore.Mvc;
using DisasterResourceAllocationAPI.Interfaces;
using DisasterResourceAllocationAPI.Models;
// using System.Text.Json;

namespace DisasterResourceAllocationAPI.Controllers
{
    [Route("api/areas")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly ILogger<AreaController> _logger;

        public AreaController(IAreaService areaService, ILogger<AreaController> logger)
        {
            _areaService = areaService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddAreas([FromBody] List<Area> areas)
        {
            if (areas == null || !areas.Any())
            {
                return BadRequest("Areas cannot be null or empty");
            }
            // _logger.LogInformation("Received Areas: {@Areas}", JsonSerializer.Serialize(areas, new JsonSerializerOptions { WriteIndented = true }));
            _areaService.AddAreas(areas);
            return Ok(new { message = "Areas added successfully" });
        }

        [HttpGet]
        public IActionResult GetAreas()
        {
            return Ok(new { message = "success", data = _areaService.GetAreas() });
        }

        [HttpDelete("{areaId}")]
        public IActionResult RemoveArea(string areaId)
        {
            var result = _areaService.RemoveArea(areaId);
            if (!result)
            {
                return NotFound($"Area with AreaID '{areaId}' not found.");
            }
            return Ok(new {  message = "Area removed successfully" });
        }
        
        [HttpPut("{areaId}")]
        public IActionResult UpdateArea(string areaId, [FromBody] Area updatedArea)
        {
            var result = _areaService.UpdateArea(areaId, updatedArea);
            if (!result)
            {
                return NotFound($"Area with AreaID '{areaId}' not found.");
            }
            return Ok(new { message = "Area updated successfully" });
        }
    }
}