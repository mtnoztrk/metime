using Metime.Models;

namespace Metime.Test.Utils
{
    public class FlightGetOffset : ICustomOffsetResolver<Flight>
    {
        public int GetOffset(Flight rootEntity, string propertyName)
        {
            if (propertyName == nameof(Flight.ArrivalDate)) return rootEntity.Arrival.Offset;
            else if (propertyName == nameof(Flight.DepartureDate)) return rootEntity.Departure.Offset;
            else throw new System.Exception("No bueno");
        }
    }
}
