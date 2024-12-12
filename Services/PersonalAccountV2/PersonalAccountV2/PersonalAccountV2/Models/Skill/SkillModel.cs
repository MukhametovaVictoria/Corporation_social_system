using BS.Contracts.Base;
using PersonalAccountV2.Models.Employee;

namespace PersonalAccountV2.Models.Skill
{
    public class SkillModel
    {
        public Guid Id { get; set; }
        public TypeSkill Type { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public EmployeeModel? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
