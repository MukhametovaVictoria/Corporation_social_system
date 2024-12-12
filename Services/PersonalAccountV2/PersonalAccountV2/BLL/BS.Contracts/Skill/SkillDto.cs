using BS.Contracts.Base;
using BS.Contracts.Employee;
namespace BS.Contracts.Skill
{
    public  class SkillDto
    {
        public Guid Id { get; set; }
        public TypeSkill Type { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public EmployeeDto? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
