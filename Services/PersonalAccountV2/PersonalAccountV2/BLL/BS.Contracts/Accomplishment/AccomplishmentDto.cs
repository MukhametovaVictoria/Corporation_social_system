using BS.Contracts.Base;
using BS.Contracts.Employee;

namespace BS.Contracts.Accomplishment
{
    public class AccomplishmentDto
    {
        public Guid Id { get; set; }
        public TypeAccomplishment Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public EmployeeDto? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
