using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts.Project;
using Services.Repositories.Abstractions;

namespace Services.Implementations
{
	public class ProjectsService : IProjectService
	{
		private readonly IMapper _mapper;
		private readonly IProjectRepository _projectsRepository;

		public ProjectsService(
		   IMapper mapper,
		   IProjectRepository projectsRepository)
		{
			_mapper = mapper;
			_projectsRepository = projectsRepository;
		}

		/// <summary>
		/// Получить проект.
		/// </summary>
		/// <returns> ДТО проекта. </returns>
		public async Task<ICollection<ProjectDto>> GetAllAsync(CancellationToken cancellationToken)
		{
			var projects = await _projectsRepository.GetAllAsync(cancellationToken);
			return _mapper.Map<ICollection<Project>, ICollection<ProjectDto>>(projects);
		}

		/// <summary>
		/// Получить проект.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		/// <returns> ДТО проекта. </returns>
		public async Task<ProjectDto> GetByIdAsync(Guid id)
		{
			var project = await _projectsRepository.GetAsync(id, CancellationToken.None);
			return _mapper.Map<Project, ProjectDto>(project);
		}

		/// <summary>
		/// Создать проект.
		/// </summary>
		/// <param name="creatingProjectDto"> ДТО создаваемого проекта. </param>
		/// <returns> Идентификатор. </returns>
		public async Task<Guid> CreateAsync(CreatingProjectDto creatingProjectDto)
		{
			var project = _mapper.Map<CreatingProjectDto, Project>(creatingProjectDto);
			if (_projectsRepository.CheckCanCreate(project))
			{
                var createdProject = await _projectsRepository.AddAsync(project);
                await _projectsRepository.SaveChangesAsync();
                return createdProject.Id;
            }

			return Guid.Empty;
		}

		/// <summary>
		/// Изменить проект.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		/// <param name="updatingCourseDto"> ДТО редактируемого проект. </param>
		public async Task UpdateAsync(Guid id, UpdatingProjectDto updatingProjectDto)
		{
			var project = await _projectsRepository.GetAsync(id, CancellationToken.None);
			if (project == null)
			{
				throw new Exception($"Проект с идентфикатором {id} не найден");
			}

			project.Name = updatingProjectDto.Name;
			project.Code = updatingProjectDto.Code;
			_projectsRepository.Update(project);
			await _projectsRepository.SaveChangesAsync();
		}

		/// <summary>
		/// Удалить проект.
		/// </summary>
		/// <param name="id"> Идентификатор. </param>
		public async Task DeleteAsync(Guid id)
		{
			var project = await _projectsRepository.GetAsync(id, CancellationToken.None);
			_projectsRepository.Delete(id);
		}

		/// <summary>
		/// Получить постраничный список.
		/// </summary>
		/// <param name="filterDto"> ДТО фильтра. </param>
		/// <returns> Список проктов. </returns>
		public async Task<ICollection<ProjectDto>> GetPagedAsync(ProjectFilterDto filterDto)
		{
			ICollection<Project> entities = await _projectsRepository.GetPagedAsync(filterDto);
			return _mapper.Map<ICollection<Project>, ICollection<ProjectDto>>(entities);
		}
	}
}
