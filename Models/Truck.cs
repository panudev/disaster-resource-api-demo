namespace DisasterResourceAllocationAPI.Models
{
    public class Truck
    {
        public string? TruckID { get; set; }
        public Dictionary<string, int> AvailableResource { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> TravelTimeArea { get; set; }= new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    }
}