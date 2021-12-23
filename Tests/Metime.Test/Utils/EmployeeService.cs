using Metime.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metime.Test.Utils
{
    [ConvertTimezone]
    public class EmployeeService
    {
        private readonly TestDbContext _dbContext;
        public EmployeeService(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> InsertAsync (Employee user)
        {
            await _dbContext.Employees.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public Task<Employee> GetAsync(int id)
        {
            return _dbContext.Employees.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
