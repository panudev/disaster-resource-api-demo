using DisasterResourceAllocationAPI.Models;

namespace DisasterResourceAllocationAPI.Interfaces
{
    public interface IAreaService
    {
        void AddAreas(List<Area> area);
        bool RemoveArea(string areaId);
        List<Area> GetAreas();
        bool UpdateArea(string areaId, Area updatedData);
    }
}