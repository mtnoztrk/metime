using Metime.Test.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Metime.Test.Unit
{
    public class GetTests
    {
        private readonly EmployeeService _eService;
        private readonly FlightService _flService;
        private readonly AirportService _aService;
        public GetTests(
            EmployeeService employeeService,
            FlightService flightService,
            AirportService airportService)
        {
            _eService = employeeService;
            _flService = flightService;
            _aService = airportService;
        }

        [Fact]
        public async Task Get_Employee()
        {
            var employee = await _eService.GetAsync(-1);
            Assert.True(employee.BirthDate == new DateTime(1991, 12, 8));
            Assert.True(employee.ShiftEnd == new TimeSpan(18, 0, 0));
            Assert.True(employee.ShiftStart == new TimeSpan(9, 0, 0));
            Assert.True(employee.CreatedAt == new DateTime(2021, 1, 1, 23, 0, 0));
        }

        [Fact]
        public async Task Get_Flight()
        {
            var flight = await _flService.GetAsync(-1);
            Assert.True(flight.CreatedAt == new DateTime(2021, 1, 1, 23, 0, 0));
            Assert.True(flight.DepartureDateTime == new DateTime(2020, 3, 3, 18, 0, 0));
            Assert.True(flight.ArrivalDateTime == new DateTime(2020, 3, 3, 18, 15, 0));
        }

        [Fact]
        public async Task Get_Airport()
        {
            var airport = await _aService.GetAsync(-1);
            var flight = airport.Outbound.First();
            Assert.True(flight.CreatedAt == new DateTime(2021, 1, 1, 23, 0, 0));
            Assert.True(flight.DepartureDateTime == new DateTime(2020, 3, 3, 18, 0, 0));
            Assert.True(flight.ArrivalDateTime == new DateTime(2020, 3, 3, 18, 15, 0));
        }
    }
}
