using Metime.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Metime.Extensions
{
    public static class MetimeExtensions
    {
        public static IApplicationBuilder UseMetime(this IApplicationBuilder app) {
            var timezoneSetting = app.ApplicationServices.GetService<TimezoneServiceProvider>();
            ServiceLocator.RegisterService(timezoneSetting);
            return app;
        }

        public static IServiceCollection AddMetime<T>(this IServiceCollection services)
            where T : class, ICanGetOffset
        {
            services.AddSingleton<TimezoneServiceProvider>();
            services.AddScoped<ICanGetOffset, T>();
            return services;
        }
    }
}
