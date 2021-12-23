using Metime.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metime.Test.Utils
{
    [ConvertTimezone]
    public class FlightService
    {
        private readonly TestDbContext _dbContext;
        public FlightService(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Flight>> GetAsync()
        {
            return _dbContext.Flights.ToListAsync();
        }
    }
}
