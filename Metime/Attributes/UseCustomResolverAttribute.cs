using System;
using System.Runtime.CompilerServices;

namespace Metime.Attributes
{
    /// <summary>
    /// <see cref="UseCustomResolverAttribute"/> Converter uses custom resolver for this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UseCustomResolverAttribute : Attribute
    {
        public string PropertyName { get; set; }

        public UseCustomResolverAttribute([CallerMemberName] string propertyName = null)
        {
            PropertyName = propertyName;
        }
    }
}
