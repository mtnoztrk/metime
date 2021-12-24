using Metime.Attributes;
using Metime.Models;

namespace Metime.Example.Services
{
    [ConvertTimezone]
    public class FlightService
    {
        public Flight GetSingleFlight()
        {
            var flight = new Flight
            {
                Id = 1,
                Code = "TK0069",
                CreatedAt = new DateTime(2020, 1, 1, 20, 0, 0), // default offset : 60
                DepartureId = 1,
                DepartureDateTime = new DateTime(2020, 3, 3, 15, 0, 0),
                Departure = new Airport
                {
                    Id = 1,
                    Code = "IST",
                    OffsetInMinutes = 180,
                },
                ArrivalId = 2,
                ArrivalDateTime = new DateTime(2020, 3, 3, 16, 15, 0),
                Arrival = new Airport
                {
                    Id = 2,
                    Code = "AMS",
                    OffsetInMinutes = 120,
                },
                FlightDay = new DateTime(2020, 3, 3),
            };

            return flight;
        }
    }
}
