using Metime.Models;

namespace Metime.Example.TimezoneResolvers
{
    public class FlightResolver : ICustomOffsetResolver<Flight>
    {
        public int GetOffset(Flight rootEntity, string propertyName)
        {
            // custom timezone resolve logic below
            return propertyName switch
            {
                nameof(Flight.ArrivalDateTime) => rootEntity.Arrival?.OffsetInMinutes,
                nameof(Flight.DepartureDateTime) => rootEntity.Departure?.OffsetInMinutes,
                _ => throw new Exception("Property not implemented yet."),
            } ?? throw new Exception("Airport not included.");
        }
    }
}
