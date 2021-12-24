using Metime.Example.DTOs;
using Metime.Example.Services;
using Metime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Metime.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        [HttpPost()]
        public ActionResult<EmployeeResponse> Insert([FromBody] EmployeeRequest request)
        {

            var employee = new Employee();
            employee.Name = request.Name;
            employee.BirthDate = request.BirthDate;
            employee.ShiftStart = request.ShiftStart.TimeOfDay;
            employee.ShiftEnd = request.ShiftEnd?.TimeOfDay;
            // this line is important, should be automatically done with something like automapper in real projects
            employee.Kind = request.Kind; 

            new EmployeeService().Insert(employee);

            var response = new EmployeeResponse();
            response.Name = employee.Name;
            response.BirthDate = employee.BirthDate;
            response.ShiftStart = employee.ShiftStart;
            response.ShiftEnd = employee.ShiftEnd;
            response.CreatedAt = employee.CreatedAt;
            response.UpdatedAt = employee.UpdatedAt;
            return response;
        }
    }
}