using Metime.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Metime
{
    /// <summary>
    /// Decorator for using scoped services within ICanGetOffset implemetations.
    /// </summary>
    internal class TimezoneServiceProvider : ICanGetOffset
    {
        private IServiceScopeFactory _serviceScopeFactory;
        public TimezoneServiceProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public int GetOffset(object rootEntity)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ICanGetOffset>();
                return service.GetOffset(rootEntity);
            }
        }

        /// <summary>
        /// custom offest resolver. Used with <see cref="ConvertWithAttribute"/>
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
