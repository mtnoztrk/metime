using Metime.Example.Services;
using Metime.Models;
using Microsoft.AspNetCore.Mvc;

namespace Metime.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Flight> Get([FromRoute] int id)
        {
            if (id != 1) return NotFound();
            return new FlightService().GetSingleFlight();
        }
    }
}