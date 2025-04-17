using DisasterResourceAllocationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterResourceAllocationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Truck> Trucks { get; set; }
    }
}