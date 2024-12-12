namespace BS.Contracts.Skill
{
    public class CreatingOrUpdatingSkillDto
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
