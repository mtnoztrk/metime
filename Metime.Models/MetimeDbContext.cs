using Microsoft.EntityFrameworkCore;

namespace Metime.Models
{
    public class MetimeDbContext : DbContext
    {
        public MetimeDbContext()
        {

        }

        public MetimeDbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>().HasMany(a => a.Inbound).WithOne(f => f.Arrival);
            modelBuilder.Entity<Airport>().HasMany(a => a.Outbound).WithOne(f => f.Departure);
            base.OnModelCreating(modelBuilder);
        }
    }
}
