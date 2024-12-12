using BS.Contracts.Employee;

namespace BS.Contracts.Event
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAcrive { get; set; }
        public EmployeeDto? Employee { get; set; }
        public Guid EmployeeId { get; set; }

    }
}
