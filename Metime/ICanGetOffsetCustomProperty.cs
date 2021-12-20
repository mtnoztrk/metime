namespace Metime
{
    public interface ICanGetOffsetCustomProperty : ICanGetOffset
    {
        /// <summary>
        /// Entity using this resolver.
        /// </summary>
        string EntityName { get; init; }
        /// <summary>
        /// Properties of the entity that are using this resolver.
        /// </summary>
        string[] PropertyNames { get; init; }
    }
}
