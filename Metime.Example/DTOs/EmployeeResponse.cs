namespace Metime.Example.DTOs
{
    public class EmployeeResponse
    {
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan? ShiftEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
