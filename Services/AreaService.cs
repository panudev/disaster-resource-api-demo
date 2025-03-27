using DisasterResourceAllocationAPI.Interfaces;
using DisasterResourceAllocationAPI.Models;
using JsonFlatFileDataStore;
// using System.Text.Json;

namespace DisasterResourceAllocationAPI.Services
{
    public class AreaService : IAreaService
    {
        private readonly IDocumentCollection<Area> _areas;
        private readonly ILogger<AreaService> _logger;
        public AreaService(ILogger<AreaService> logger)
        {
            _logger = logger;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "areas.json");
            var store = new DataStore(path);
            _areas = store.GetCollection<Area>("areas");
        }

        public void AddAreas(List<Area> areas)
        {
            var areaList = areas.Where(a => !_areas.AsQueryable().Any(_a => _a.AreaID == a.AreaID)).ToList();
            _areas.InsertMany(areaList);
        }

        public bool RemoveArea(string areaId)
        {
            var deleted = _areas.DeleteOne(a => a.AreaID == areaId);
            return deleted;
        }

        public List<Area> GetAreas()
        {
            return _areas.AsQueryable().ToList();
        }

        public bool UpdateArea(string areaId, Area updatedData)
        {
            var updated = _areas.UpdateOne(a => a.AreaID == areaId, updatedData);
            return updated;
        }
    }
}