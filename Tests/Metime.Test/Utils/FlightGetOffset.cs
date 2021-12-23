namespace Metime.Test.Utils
{
    public class FlightGetOffset : ICanGetOffsetCustom<Flight>
    {
        public int GetOffset(Flight rootEntity, string propertyName)
        {
            if (propertyName == "Arrival") return rootEntity.ArrivalOffset;
            else if (propertyName == "Departure") return rootEntity.DepartureOffset;
            else throw new System.Exception("No bueno");
        }
    }
}
