using Metime.Attributes;
using System;

namespace Metime.Test.Utils
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        [IgnoreTimezone]
        public DateTime BirthDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan? ShiftEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
