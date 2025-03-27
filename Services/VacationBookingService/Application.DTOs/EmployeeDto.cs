namespace Application.DTOs
{
    public class EmployeeDto : EntityDto
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
    }
}
