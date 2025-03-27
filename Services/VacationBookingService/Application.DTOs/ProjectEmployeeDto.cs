namespace Application.DTOs
{
    public class ProjectEmployeeDto : EntityDto
    {
        public Guid EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectDto? Project { get; set; }
    }
}