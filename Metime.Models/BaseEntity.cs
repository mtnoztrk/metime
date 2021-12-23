using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metime.Models
{
    public class BaseEntity : ITimezoneConvertible
    {
        [Key]
        public long Id { get; set; }

        [NotMapped] public TimezoneFormat Kind { get; set; } = TimezoneFormat.UTC;
    }
}
