namespace Domain.Entities
{
    public class Project : Entity
    {
        public required string Name { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    }
}