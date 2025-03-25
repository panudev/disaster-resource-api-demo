using Microsoft.AspNetCore.Mvc;
using DisasterResourceAllocationAPI.Interfaces;
using DisasterResourceAllocationAPI.Models;
using System.Text.Json;

namespace DisasterResourceAllocationAPI.Controllers
{
    [Route("api/trucks")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckService _truckService;
        private readonly ILogger<TrucksController> _logger;

        public TrucksController(ITruckService truckService, ILogger<TrucksController> logger)
        {
            _truckService = truckService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddTrucks([FromBody] List<Truck> trucks)
        {
            if (trucks == null || !trucks.Any())
            {
                return BadRequest("Trucks cannot be null or empty");
            }
            // _logger.LogInformation("Received Trucks: {@Trucks}", JsonSerializer.Serialize(trucks, new JsonSerializerOptions { WriteIndented = true }));
            _truckService.AddTrucks(trucks);
            return Ok("Trucks added successfully");
        }

        [HttpGet]
        public IActionResult GetAllTrucks()
        {
            return Ok(_truckService.GetAllTrucks());
        }

        [HttpDelete("{truckId}")]
        public IActionResult RemoveTruck(string truckId)
        {
            var result = _truckService.RemoveTruck(truckId);
            if (!result)
            {
                return NotFound($"Truck with TruckID '{truckId}' not found.");
            }
            return Ok("Truck removed successfully");
        }

        [HttpPut("{truckId}")]
        public IActionResult UpdateTruck(string truckId, [FromBody] Truck updatedTruck)
        {
            var result = _truckService.UpdateTruck(truckId, updatedTruck);
            if (!result)
            {
                return NotFound($"Truck with TruckID '{truckId}' not found.");
            }
            return Ok("Truck updated successfully");
        }
    }
}