namespace Domain.Entities
{
    public class Employee : Entity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    }
}