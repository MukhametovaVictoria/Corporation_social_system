using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> AddProjectAsync(Project project);
        Task<Project> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Guid id);
        Task<IEnumerable<Project>> GetProjectsByEmployeeIdAsync(Guid employeeId);
    }
}
