using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectDto? ToDto(Project project)
        {
            if (project == null)
                return null; 
            
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                ModifiedOn = project.ModifiedOn,
                CreatedOn = project.CreatedOn
            };
        }

        public static Project? ToEntity(ProjectDto projectDto)
        {
            if (projectDto == null)
                return null;

            return new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                ModifiedOn = projectDto.ModifiedOn,
                CreatedOn = projectDto.CreatedOn
            };
        }
    }
}