using DisasterResourceAllocationAPI.Models;

namespace DisasterResourceAllocationAPI.Interfaces
{
    public interface IAssignmentService
    {
        List<Assignment> AssignTrucksToAreas();
    }
}