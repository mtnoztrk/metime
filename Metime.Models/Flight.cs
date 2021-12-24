using Metime.Attributes;

namespace Metime.Models
{
    public class Flight : BaseEntity
    {
        public string Code { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        [UseCustomResolver]
        public DateTime DepartureDateTime { get; set; }
        public int DepartureId { get; set; }
        public Airport? Departure { get; set; }

        [UseCustomResolver]
        public DateTime ArrivalDateTime { get; set; }

        public int ArrivalId { get; set; }
        public Airport? Arrival { get; set; }
        [IgnoreTimezone]
        public DateTime FlightDay { get; set; }
    }
}
