namespace Metime.Test.Utils
{
    public class FixedGetOffset : IOffsetResolver
    {
        public int GetOffset(object rootEntity)
        {
            return 180;
        }
    }
}
