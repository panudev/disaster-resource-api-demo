namespace DisasterResourceAllocationAPI.Models
{
    public class RedisConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Password { get; set; }
        public bool UseSsl { get; set; }
    }
}