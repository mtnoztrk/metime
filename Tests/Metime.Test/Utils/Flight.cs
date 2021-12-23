using Metime.Attributes;
using System;

namespace Metime.Test.Utils
{
    public class Flight : BaseEntity
    {
        public string Code { get; set; }
        public int DepartureOffset { get; set; }
        [UseCustomResolver]
        public DateTime Departure { get; set; }
        public int ArrivalOffset { get; set; }
        [UseCustomResolver]
        public DateTime Arrival { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
