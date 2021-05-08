using System;

namespace Metime.Attributes
{
    /// <summary>
    /// Use specific offset resolver service for this property.
    /// <para>Resolver service must implement <see cref="ICanGetOffset"/></para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConvertWithAttribute : Attribute
    {
        internal Type resolverService;
        public ConvertWithAttribute(Type resolverService)
        {
            this.resolverService = resolverService;
        }
    }
}
