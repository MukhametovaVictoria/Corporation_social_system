using AutoMapper;
using Domain.Entities;
using Services.Contracts.Project;

namespace Services.Implementations.Mapping
{
	public class ProjectsMapping : Profile
	{
		public ProjectsMapping()
		{
			CreateMap<Project, ProjectDto>();

			CreateMap<CreatingProjectDto, Project>()
				.ForMember(d => d.Id, map => map.Ignore())
				.ForMember(d => d.TimeTrackers, map => map.Ignore());

			CreateMap<UpdatingProjectDto, Project>()
				.ForMember(d => d.Id, map => map.Ignore())
				.ForMember(d => d.TimeTrackers, map => map.Ignore());
		}
	}
}
