namespace Metime.Models
{
    public class Airport : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Offset { get; set; }
        public ICollection<Flight>? Outbound { get; set; }
        public ICollection<Flight>? Inbound { get; set; }
    }
}
