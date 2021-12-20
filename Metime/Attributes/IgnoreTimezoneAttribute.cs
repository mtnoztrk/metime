using System;

namespace Metime.Attributes
{
    /// <summary>
    /// <see cref="ConvertTimezoneAttribute"/> Ignores THIS DateTime property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreTimezoneAttribute : Attribute
    {
    }
}
