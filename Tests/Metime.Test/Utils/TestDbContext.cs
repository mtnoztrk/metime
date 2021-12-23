﻿using Metime.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Metime.Test.Utils
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {

        }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>().HasMany(a => a.Inbound).WithOne(f => f.Arrival);
            modelBuilder.Entity<Airport>().HasMany(a => a.Outbound).WithOne(f => f.Departure);
            modelBuilder.Entity<Airport>().HasData(new List<Airport>() {
                new Airport
                {
                    Id = -1,
                    Name = "IST",
                    Offset = 180,
                },
                new Airport
                {
                    Id = -2,
                    Name = "AMS",
                    Offset = 120,
                },
            });

            modelBuilder.Entity<Flight>().HasData(new List<Flight>() {
                new Flight
                {
                    Id = -1,
                    Code = "TK0069",
                    CreatedAt = new DateTime(2021, 1, 1, 20, 0, 0),
                    DepartureId = -1,
                    DepartureDate = new DateTime(2020, 3, 3, 15, 0, 0),
                    ArrivalId = -2,
                    ArrivalDate = new DateTime(2020, 3, 3, 16, 15, 0),
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

        }
    }
}
