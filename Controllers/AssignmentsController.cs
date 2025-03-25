

using Microsoft.AspNetCore.Mvc;
using DisasterResourceAllocationAPI.Interfaces;

namespace DisasterResourceAllocationAPI.Controllers
{
    [Route("api/assignments")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IRedisService _redisService;
        private readonly ILogger<AssignmentsController> _logger;

        public AssignmentsController(IAssignmentService assignmentService, IRedisService redisService, ILogger<AssignmentsController> logger)
        {
            _assignmentService = assignmentService;
            _redisService = redisService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AssignTrucksToAreas()
        {
            var assignments = _assignmentService.AssignTrucksToAreas();
            await _redisService.AddAssignments(assignments);
            return Ok(assignments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssignments()
        {
            var assignments = await _redisService.GetAllAssignments();
            return Ok(assignments);
        }

        [HttpDelete]
        public async Task<IActionResult> ClearAssignments()
        {
            await _redisService.ClearAssignments();
            return Ok("Assignments cleared successfully");
        }
    }
}