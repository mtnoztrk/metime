using Microsoft.Extensions.DependencyInjection;
using System;

namespace Metime
{
    /// <summary>
    /// Decorator for using scoped services within ICanGetOffset implemetations.
    /// </summary>
    internal class TimezoneServiceProvider
    {
        private IServiceScopeFactory _serviceScopeFactory;
        public TimezoneServiceProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        /// <summary>
        /// gets offset resolver with type.
        /// </summary>
        /// <param name="rootEntity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetOffset(object rootEntity, Type type)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var service = (ICanGetOffset)scope.ServiceProvider.GetRequiredService(type);
                return service.GetOffset(rootEntity);
            }
        }
    }
}
