namespace DisasterResourceAllocationAPI.Models
{
    public class Area
    {
        public string? AreaID { get; set; }
        public int? UrgencyLevel { get; set; }

        public Dictionary<string, int> RequiredResource { get; set; } = new Dictionary<string, int>();
        public  int? TimeConstraint { get; set; }

    }
}