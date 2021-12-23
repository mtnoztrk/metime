using Microsoft.Extensions.DependencyInjection;

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
        /// finds offset with default resolver
        /// </summary>
        /// <param name="rootEntity"></param>
        /// <returns></returns>
        public int GetOffset(object rootEntity)
        {
            //TODO: cache these services, don't create scope all the time.
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var defaultService = scope.ServiceProvider.GetRequiredService<IOffsetResolver>();
                return defaultService.GetOffset(rootEntity);
            }
        }

        /// <summary>
        /// finds offset with custom resolver
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rootEntity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public int GetCustomOffset<T>(T rootEntity, string propertyName) where T : class, ITimezoneConvertible
        {
            //TODO: cache these services, don't create scope all the time.
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ICustomOffsetResolver<T>>();
                return service.GetOffset(rootEntity, propertyName);
            }
        }
    }
}
