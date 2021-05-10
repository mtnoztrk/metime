namespace Metime.Test.Utils
{
    public class FixedGetOffset : ICanGetOffset
    {
        public int GetOffset(object rootEntity)
        {
            return 180;
        }
    }
}
