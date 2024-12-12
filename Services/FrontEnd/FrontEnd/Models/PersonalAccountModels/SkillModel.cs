namespace FrontEnd.Models.PersonalAccountModels
{
    public class SkillModel
    {
        public Guid Id { get; set; }
        public TypeSkill Type { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public EmployeeModelFromPA? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
