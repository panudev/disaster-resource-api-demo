using DisasterResourceAllocationAPI.Models;

namespace DisasterResourceAllocationAPI.Interfaces
{
    public interface ITruckService
    {
        void AddTrucks(List<Truck> truck);
        bool RemoveTruck(string truckId);
        List<Truck> GetAllTrucks();
        bool UpdateTruck(string truckId,Truck updatedData);
    }
}