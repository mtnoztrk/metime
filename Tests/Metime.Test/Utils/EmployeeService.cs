using Metime.Attributes;
using Metime.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Metime.Test.Utils
{
    [ConvertTimezone]
    public class EmployeeService
    {
        public async Task<Employee> InsertAsync(Employee user)
        {
            using (var ctx = new TestDataContextFactory().Create())
            {
                await ctx.Employees.AddAsync(user);
                await ctx.SaveChangesAsync();
                return user;
            }
        }

        public Task<Employee> GetAsync(int id)
        {
            using (var ctx = new TestDataContextFactory().Create())
            {
                return ctx.Employees.FirstOrDefaultAsync(c => c.Id == id);
            }
        }
    }
}
