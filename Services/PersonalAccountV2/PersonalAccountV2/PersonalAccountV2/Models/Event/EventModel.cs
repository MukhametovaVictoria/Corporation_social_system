using PersonalAccountV2.Models.Employee;

namespace PersonalAccountV2.Models.Event
{

    public class EventModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAcrive { get; set; }
        public EmployeeModel? Employee { get; set; }
        public Guid EmployeeId { get; set; }

    }
}
