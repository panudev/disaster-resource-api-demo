using DisasterResourceAllocationAPI.Interfaces;
using DisasterResourceAllocationAPI.Models;
using JsonFlatFileDataStore;

namespace DisasterResourceAllocationAPI.Services
{
    public class TruckService : ITruckService
    {
        private readonly IDocumentCollection<Truck> _trucks;
        private readonly ILogger<TruckService> _logger;

        public TruckService(ILogger<TruckService> logger)
        {
            _logger = logger;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "trucks.json");
            var store = new DataStore(path);
            _trucks = store.GetCollection<Truck>("trucks");
        }

        public void AddTrucks(List<Truck> trucks)
        {
            var truckList = trucks.Where(t => !_trucks.AsQueryable().Any(t => t.TruckID == t.TruckID)).ToList();
            _trucks.InsertMany(truckList);
        }

        public bool RemoveTruck(string truckId)
        {
            var deleted = _trucks.DeleteOne(t => t.TruckID == truckId); 
            return deleted; 
        }
        
        public List<Truck> GetAllTrucks()
        {
            return _trucks.AsQueryable().ToList();
        }

        public bool UpdateTruck(string truckId, Truck updatedTruck)
        {
            var updated = _trucks.UpdateOne(t => t.TruckID == truckId, updatedTruck);
            return updated;
        }
        
    }
}