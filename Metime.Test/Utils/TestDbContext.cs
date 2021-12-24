using Metime.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Metime.Test.Utils
{
    public class TestDbContext : MetimeDbContext
    {
        public TestDbContext()
        {

        }

        public TestDbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>().HasData(new List<Airport>() {
                new Airport
                {
                    Id = -1,
                    Code = "IST",
                    OffsetInMinutes = 180,
                },
                new Airport
                {
                    Id = -2,
                    Code = "AMS",
                    OffsetInMinutes = 120,
                },
            });

            modelBuilder.Entity<Flight>().HasData(new List<Flight>() {
                new Flight
                {
                    Id = -1,
                    Code = "TK0069",
                    CreatedAt = new DateTime(2021, 1, 1, 20, 0, 0),
                    DepartureId = -1,
                    DepartureDateTime = new DateTime(2020, 3, 3, 15, 0, 0),
                    ArrivalId = -2,
                    ArrivalDateTime = new DateTime(2020, 3, 3, 16, 15, 0),
                }
            });

            modelBuilder.Entity<Employee>().HasData(new List<Employee>() {
                new Employee
                {
                    Id = -1,
                    Name = "Metin",
                    BirthDate = new DateTime(1991, 12, 8),
                    CreatedAt = new DateTime(2021, 1, 1, 20, 0, 0),
                    ShiftEnd = new TimeSpan(15, 0, 0),
                    ShiftStart = new TimeSpan(6, 0, 0)
                }
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
