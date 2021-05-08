namespace Metime
{
    public interface ICanGetOffset
    {
        /// <summary>
        /// gets offset.
        /// </summary>
        /// <param name="rootEntity">root object being processed</param>
        /// <returns></returns>
        public int GetOffset(object rootEntity);
    }
}
