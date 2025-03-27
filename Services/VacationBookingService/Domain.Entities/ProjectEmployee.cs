namespace Domain.Entities
{
    public class ProjectEmployee : Entity
    {
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}