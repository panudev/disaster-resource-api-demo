using DisasterResourceAllocationAPI.Models;

namespace DisasterResourceAllocationAPI.Interfaces
{
    public interface IRedisService
    {
        Task<List<Assignment>> GetAllAssignments();
        Task AddAssignments(List<Assignment> assignments);
        Task ClearAssignments();
    }
}