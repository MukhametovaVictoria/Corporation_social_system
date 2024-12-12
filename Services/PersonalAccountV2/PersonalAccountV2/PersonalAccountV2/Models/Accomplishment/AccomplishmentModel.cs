using BS.Contracts.Base;
using PersonalAccountV2.Models.Employee;

namespace PersonalAccountV2.Models.Accomplishment
{
    public class AccomplishmentModel
    {
        public Guid Id { get; set; }
        public TypeAccomplishment Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public EmployeeModel? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }

}
