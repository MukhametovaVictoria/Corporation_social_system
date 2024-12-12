using BS.Contracts.Base;

namespace PersonalAccountV2.Models.Skill
{
    public class CreatingOrUpdatingSkillModel
    {
        public Guid Id { get; set; }
        public TypeSkill Type { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
