namespace Metime
{
    public interface ICanGetOffsetCustom<T> : ICanGetOffset where T : class, ITimezoneConvertible
    {
        /// <summary>
        /// Properties of the entity that are using this resolver.
        /// </summary>
        string[] PropertyNames { get; init; }
    }
}
