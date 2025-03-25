namespace DisasterResourceAllocationAPI.Models
{
    public class Assignment
    {
        public string? AreaID { get; set; }
        public string? TruckID { get; set; }
        public Dictionary<string, int> ResourceDelivered { get; set; } = new Dictionary<string, int>();
        public string? Message { get; set; }
    }
}