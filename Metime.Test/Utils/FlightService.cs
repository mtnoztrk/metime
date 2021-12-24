using Metime.Attributes;
using Metime.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Metime.Test.Utils
{
    [ConvertTimezone]
    public class FlightService
    {
        public async Task<Flight> GetAsync(int id)
        {
            using (var ctx = new TestDataContextFactory().Create())
            {
                var ports = await ctx.Airports.ToListAsync();
                var flight = await ctx.Flights
                    .Include(c => c.Arrival)
                    .Include(c => c.Departure)
                    .SingleAsync(c => c.Id == id);
                flight.Arrival = ports.First(c => c.Id == flight.ArrivalId);
                flight.Departure = ports.First(c => c.Id == flight.DepartureId);
                return flight;
            }
        }
    }
}
