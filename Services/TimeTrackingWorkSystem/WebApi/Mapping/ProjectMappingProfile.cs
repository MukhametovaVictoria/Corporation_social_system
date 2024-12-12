using AutoMapper;
using Services.Contracts.Project;
using WebApi.Models.Project;

namespace WebApi.Mapping
{
	/// <summary>
	/// Профиль автомаппера для сущности проекта.
	/// </summary>
	public class ProjectsMappingProfile : Profile
	{
		public ProjectsMappingProfile() 
		{
			CreateMap<ProjectDto, ProjectModel>();
			CreateMap<CreatingProjectModel, CreatingProjectDto>();
			CreateMap<UpdatingProjectModel, UpdatingProjectDto>();
			CreateMap<ProjectFilterModel, ProjectFilterDto>();
		}
	}
}
