using Metime.Models;

namespace Metime.Test.Utils
{
    public class FlightGetOffset : ICustomOffsetResolver<Flight>
    {
        public int GetOffset(Flight rootEntity, string propertyName)
        {
            if (propertyName == nameof(Flight.ArrivalDateTime)) return rootEntity.Arrival.OffsetInMinutes;
            else if (propertyName == nameof(Flight.DepartureDateTime)) return rootEntity.Departure.OffsetInMinutes;
            else throw new System.Exception("No bueno");
        }
    }
}
