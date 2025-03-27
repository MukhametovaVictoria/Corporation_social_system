using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            return await _projectRepository.AddProjectAsync(project);
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await _projectRepository.GetProjectByIdAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllProjectsAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectRepository.DeleteProjectAsync(id);
        }

        public async Task<IEnumerable<Project>> GetProjectsByEmployeeIdAsync(Guid employeeId)
        {
            return await _projectRepository.GetProjectsByEmployeeIdAsync(employeeId);
        }
    }
}
