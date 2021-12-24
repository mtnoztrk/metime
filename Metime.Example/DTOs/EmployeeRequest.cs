namespace Metime.Example.DTOs
{
    public class EmployeeRequest : BaseRequest
    {
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime? ShiftEnd { get; set; }
    }
}
