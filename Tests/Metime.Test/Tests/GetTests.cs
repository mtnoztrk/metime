using Metime.Test.Utils;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Metime.Test.Tests
{
    public class GetTests
    {
        private readonly TestDbContext _dbContext;
        private readonly EmployeeService _service;
        private readonly FlightService _flService;
        public GetTests(
            TestDbContext dbContext,
            EmployeeService employeeService,
            FlightService flightService)
        {
            _dbContext = dbContext;
            _service = employeeService;
            _flService = flightService;
        }

        [Fact]
        public async Task Get_1()
        {
            var emp = new Employee
            {
                Id = 44,
                Name = "Metin",
                BirthDate = new DateTime(1991, 12, 8),
                CreatedAt = new DateTime(2021, 1, 1, 20, 0, 0),
                ShiftEnd = new TimeSpan(15, 0, 0),
                ShiftStart = new TimeSpan(6, 0, 0)
            };
            await _dbContext.AddAsync(emp);
            await _dbContext.SaveChangesAsync();
            var employee = await _service.GetAsync(44);
            Assert.True(employee.BirthDate == new DateTime(1991, 12, 8));
            Assert.True(employee.ShiftEnd == new TimeSpan(18, 0, 0));
            Assert.True(employee.ShiftStart == new TimeSpan(9, 0, 0));
            Assert.True(employee.CreatedAt == new DateTime(2021, 1, 1, 23, 0, 0));
        }

        [Fact]
        public async Task Get_2()
        {
            var fl = new Flight
            {
                Code = "TK0023",
                CreatedAt = new DateTime(2021, 1, 1, 20, 0, 0),
                DepartureOffset = -60,
                Departure = new DateTime(2020, 3, 3, 15, 0, 0),
                ArrivalOffset = 240,
                Arrival = new DateTime(2020, 3, 3, 17, 0, 0),
            };
            await _dbContext.AddAsync(fl);
            await _dbContext.SaveChangesAsync();
            var flights = await _flService.GetAsync();
            var flight = flights[0];
            Assert.True(flight.CreatedAt == new DateTime(2021, 1, 1, 23, 0, 0));
            Assert.True(flight.Departure == new DateTime(2020, 3, 3, 14, 0, 0));
            Assert.True(flight.Arrival == new DateTime(2020, 3, 3, 21, 0, 0));
        }
    }
}
