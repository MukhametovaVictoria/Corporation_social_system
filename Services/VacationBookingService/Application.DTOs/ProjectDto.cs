using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class ProjectDto : EntityDto
    {
        public required string Name { get; set; }
        public ICollection<ProjectEmployeeDto> ProjectEmployees { get; set; } = new List<ProjectEmployeeDto>();
    }
}