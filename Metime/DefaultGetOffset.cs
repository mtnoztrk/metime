using Metime.Models;
using Microsoft.Extensions.Options;

namespace Metime
{
    /// <summary>
    /// <see cref="TimezoneOption"/> shoud be injected before this. GmtOffsetInMin can be defined inside appsettings.json with this approach.
    /// </summary>
    public class DefaultGetOffset : ICanGetOffset
    {
        private readonly TimezoneOption option;
        public DefaultGetOffset(IOptions<TimezoneOption> options)
        {
            option = options.Value;
        }

        public int GetOffset()
        {
            return option.GmtOffsetInMin;
        }
    }
}
