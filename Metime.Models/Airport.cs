namespace Metime.Models
{
    public class Airport : BaseEntity
    {
        public string Code { get; set; } = null!;
        public int OffsetInMinutes { get; set; }
        public ICollection<Flight>? Outbound { get; set; }
        public ICollection<Flight>? Inbound { get; set; }
    }
}
