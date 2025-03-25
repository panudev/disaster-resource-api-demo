using DisasterResourceAllocationAPI.Interfaces;
using StackExchange.Redis;
using DisasterResourceAllocationAPI.Models;
using Newtonsoft.Json;


namespace DisasterResourceAllocationAPI.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _redis;
        private const string RedisKey = "assignments_result";
        private const int RedisExpiration = 30; // 30 minutes

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task<List<Assignment>> GetAllAssignments()
        {
            var assignments = await _redis.StringGetAsync(RedisKey);
            if (assignments.IsNullOrEmpty)
            {
                return new List<Assignment>();
            }
            return JsonConvert.DeserializeObject<List<Assignment>>(assignments.ToString()) ?? new List<Assignment>();
        }

        public async Task AddAssignments(List<Assignment> assignments)
        {
            var assignmentsJson = JsonConvert.SerializeObject(assignments);
            await _redis.StringSetAsync(RedisKey, assignmentsJson, TimeSpan.FromMinutes(RedisExpiration));
        }

        public async Task ClearAssignments()
        {
            await _redis.KeyDeleteAsync(RedisKey);
        }
        
    }
}