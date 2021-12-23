using Metime.Attributes;

namespace Metime.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;

        [IgnoreTimezone]
        public DateTime BirthDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan? ShiftEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
