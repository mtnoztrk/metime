using Metime.Extensions;
using Metime.Helpers;
using Metime.Models;
using Metime.Test.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Metime.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<EmployeeService>();
            services.AddTransient<FlightService>();
            services.AddTransient<AirportService>();

            services.AddMetime<FixedGetOffset>();
            services.AddCustomResolver<Flight, FlightGetOffset>();
            //below use metime replacement
            var offsetService = services.BuildServiceProvider().GetService<TimezoneServiceProvider>();
            ServiceLocator.RegisterService(offsetService);
        }
    }
}
