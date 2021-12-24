using Metime.Attributes;
using Metime.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Metime.Test.Utils
{
    [ConvertTimezone]
    public class AirportService
    {
        public async Task<Airport> GetAsync(int id)
        {
            using (var ctx = new TestDataContextFactory().Create())
            {
                var flights = await ctx.Flights.ToListAsync();

                var ports = await ctx.Airports
                    .Include(c => c.Inbound)
                    .Include(c => c.Outbound)
                    .ToListAsync();
                var port = ports.First(c => c.Id == id);

                // manually building relation because ef core in memory does different things in every version
                foreach (var flight in flights)
                {
                    if (flight.DepartureId == port.Id)
                    {
                        port.Outbound.Add(flight);
                        flight.Departure = port;
                        flight.Arrival = ports.First(c => c.Id == flight.ArrivalId);
                    }
                    else if (flight.ArrivalId == port.Id)
                    {
                        port.Inbound.Add(flight);
                        flight.Arrival = port;
                        flight.Departure = ports.First(c => c.Id == flight.DepartureId);
                    }
                }

                return port;
            }
        }
    }
}
