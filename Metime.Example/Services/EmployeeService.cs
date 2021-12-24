using Metime.Attributes;
using Metime.Models;

namespace Metime.Example.Services
{
    [ConvertTimezone]
    public class EmployeeService
    {
        private List<Employee> _employees = new List<Employee>();
        public Employee Insert(Employee employee)
        {
            employee.Id = _employees.Count + 1;
            // utc assigments should be done inside the service.
            // Otherwise it might get converted to UTC again unintentionally
            employee.CreatedAt = DateTime.UtcNow;
            _employees.Add(employee);
            return employee;
        }
    }
}
