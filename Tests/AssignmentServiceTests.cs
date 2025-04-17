// using JsonFlatFileDataStore;
// using Moq;
// using Xunit;
// using DisasterResourceAllocationAPI.Models;
// using DisasterResourceAllocationAPI.Services;


// namespace DisasterResourceAllocationAPI.Tests
// {
//     public class AssignmentServiceTests
//     {
//         [Fact]
//         public void AssignTrucksToAreasTest()
//         {
//             var areaId = "A1";
//             var truckId = "T1";

//             var mockAreaCollection = new Mock<IDocumentCollection<Area>>();
//             mockAreaCollection.Setup(x => x.AsQueryable()).Returns(new List<Area>{
//                 new Area
//                 {
//                     AreaID = areaId,
//                     UrgencyLevel = 5,
//                     RequiredResource = new Dictionary<string, int>
//                     {
//                         { "food", 200 },
//                         { "water", 300 }
//                     },
//                     TimeConstraint = 6
//                 }
//             }.AsQueryable());

//             var mockTruckCollection = new Mock<IDocumentCollection<Truck>>();
//             mockTruckCollection.Setup(x => x.AsQueryable()).Returns(new List<Truck>{
//                 new Truck
//                 {
//                     TruckID = truckId,
//                     AvailableResource = new Dictionary<string, int>
//                     {
//                         { "food", 250 },
//                         { "water", 300 }
//                     },
//                     TravelTimeArea = new Dictionary<string, int>
//                     {
//                         { areaId, 5 }
//                     }
//                 }
//             }.AsQueryable());

//             var loggerMock = new Mock<ILogger<AssignmentService>>();
//             var service = new AssignmentService(mockAreaCollection.Object, mockTruckCollection.Object, loggerMock.Object);
//             var result = service.AssignTrucksToAreas();
//             Assert.Single(result);
//             Assert.Equal(areaId, result[0].AreaID);
//             Assert.Equal(truckId, result[0].TruckID);
//             Assert.Equal("Assigned", result[0].Message);
//         }
//     }
// }