using DisasterResourceAllocationAPI.Interfaces;
using DisasterResourceAllocationAPI.Models;
using JsonFlatFileDataStore;
using System.Text.Json;

namespace DisasterResourceAllocationAPI.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IDocumentCollection<Area> _areas;
        private readonly IDocumentCollection<Truck> _trucks;
        private readonly ILogger<AssignmentService> _logger;

        public AssignmentService(ILogger<AssignmentService> logger)
        {
            _logger = logger;
            var pathArea = Path.Combine(Directory.GetCurrentDirectory(), "Data", "areas.json");
            var pathTruck = Path.Combine(Directory.GetCurrentDirectory(), "Data", "trucks.json");
            var store = new DataStore(pathArea);
            _areas = store.GetCollection<Area>("areas");
            store = new DataStore(pathTruck);
            _trucks = store.GetCollection<Truck>("trucks");
        }

        public List<Assignment> AssignTrucksToAreas()
        {
            var assignmentsResult = new List<Assignment>();

            var areas = _areas.AsQueryable().OrderByDescending(a => a.UrgencyLevel).ToList();
            // _logger.LogInformation("Areas UrgencyLevel: {@areas}", JsonSerializer.Serialize(areas, new JsonSerializerOptions { WriteIndented = true }));
            var trucks = _trucks.AsQueryable().ToList();
            // _logger.LogInformation("Trucks: {@trucks}", JsonSerializer.Serialize(trucks, new JsonSerializerOptions { WriteIndented = true }));
            foreach(var area in areas)
            {
                // _logger.LogInformation("Area: {@areas}", JsonSerializer.Serialize(area, new JsonSerializerOptions { WriteIndented = true }));
                Truck? assignTruck = null;
                int bestTravelTime = int.MaxValue;
                // สร้าง list เพื่อเก็บ truck ที่ resource พอ
                List<Truck> resourceMatchedTrucks = new();

                foreach(var truck in trucks)
                {
                    // เช็คว่าทรัพยรถพอไหม (ก่อนที่จะมีการคำนวณระยะทาง)
                    bool hasResource = true;
                    foreach(var resource in area.RequiredResource)
                    {
                        if(!truck.AvailableResource.TryGetValue(resource.Key, out int availableResource) || availableResource < resource.Value)
                        {
                            hasResource = false;
                            break;
                        }
                    }
                    if (hasResource)
                    {
                        resourceMatchedTrucks.Add(truck); // ทรัพยากรครบ → เพิ่มเข้า list
                    }
                }

                // Case 1: ไม่มี truck ไหน resource ครบเลย
                if (!resourceMatchedTrucks.Any())
                {
                    assignmentsResult.Add(new Assignment
                    {
                        AreaID = area.AreaID,
                        TruckID = null,
                        ResourceDelivered = new Dictionary<string, int>(),
                        Message = "No trucks have sufficient resources for this area."
                    });
                    continue; // ไป area ถัดไป
                }

                // เช็กว่าในกลุ่มที่ resource ครบ → ใครไปถึงทัน
                foreach(var truck in resourceMatchedTrucks)
                {
                    if(area.AreaID != null && truck.TravelTimeArea.TryGetValue(area.AreaID, out int travelTimeArea) && travelTimeArea <= area.TimeConstraint)
                    {
                        if(travelTimeArea < bestTravelTime)
                        {
                            bestTravelTime = travelTimeArea;
                            assignTruck = truck;
                        }
                    }
                }

                // Case 2: ทรัพยากรครบ แต่ไม่มี truck ไหนไปทัน
                if (assignTruck == null)
                {
                    assignmentsResult.Add(new Assignment
                    {
                        AreaID = area.AreaID,
                        TruckID = null,
                        ResourceDelivered = new Dictionary<string, int>(),
                        Message = "No trucks can reach this area in time."
                    });
                    continue;
                }

                // Assignment สำเร็จ
                assignmentsResult.Add(new Assignment
                {
                    AreaID = area.AreaID,
                    TruckID = assignTruck.TruckID,
                    ResourceDelivered = area.RequiredResource,
                    Message = "Assigned"
                });
                trucks.Remove(assignTruck);
            }
            // _logger.LogInformation("Assignments Result: {@assignmentsResult}", JsonSerializer.Serialize(assignmentsResult, new JsonSerializerOptions { WriteIndented = true }));
            return assignmentsResult;
        }
        
    }
}