using System.Text.Json.Serialization;

namespace Metime.Example.DTOs
{
    public class BaseRequest : ITimezoneConvertible
    {
        // incoming requests mostly local this can be overriden in selected requests.
        [JsonIgnore]
        public TimezoneFormat Kind { get; set; } = TimezoneFormat.Local; 
    }
}
