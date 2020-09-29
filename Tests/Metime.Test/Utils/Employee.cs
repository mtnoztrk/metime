using Metime.Attributes;
using Metime.Enums;
using Metime.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Metime.Test.Utils
{
    public class Employee : ITimezoneConvertible
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }

        [IgnoreTimezone]
        public DateTime BirthDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan? ShiftEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public TimezoneFormat Kind { get; set; } = TimezoneFormat.UTC;
    }
}
