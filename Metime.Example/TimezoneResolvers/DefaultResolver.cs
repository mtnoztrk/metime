namespace Metime.Example.TimezoneResolvers
{
    public class DefaultResolver : IOffsetResolver
    {
        public int GetOffset(object rootEntity)
        {
            // default timezone resolving logic below
            return 60;
        }
    }
}
