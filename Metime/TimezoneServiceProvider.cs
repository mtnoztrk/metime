using Microsoft.Extensions.DependencyInjection;
using System.Linq;

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
        /// gets offset resolver.
        /// </summary>
        /// <param name="rootEntity"></param>
        /// <param name="entityName"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public int GetOffset(object rootEntity, string entityName, string propertyName)
        {
            //TODO: cache these services, don't create scope all the time.
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider.GetServices<ICanGetOffsetCustomProperty>();
                foreach (var service in services)
                {
                    //TODO: add check for multiple services doing the same thing.
                    if (service.EntityName == entityName && service.PropertyNames.Contains(propertyName)) return service.GetOffset(rootEntity);
                }
                var defaultService = scope.ServiceProvider.GetRequiredService<ICanGetOffset>();
                return defaultService.GetOffset(rootEntity);
            }
        }
    }
}
