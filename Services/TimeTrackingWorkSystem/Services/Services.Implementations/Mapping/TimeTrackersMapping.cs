using AutoMapper;
using Domain.Entities;
using Services.Contracts.TimeTracker;

namespace Services.Implementations.Mapping
{
	public class TimeTrackersMapping : Profile
	{
		public TimeTrackersMapping()
		{
			CreateMap<TimeTracker, TimeTrackerDto>();

			CreateMap<CreatingTimeTrackerDto, TimeTracker>()
				.ForMember(d => d.Id, map => map.Ignore())
				.ForMember(d => d.Employee, map => map.Ignore())
				.ForMember(d => d.Project, map => map.Ignore());

			CreateMap<UpdatingTimeTrackerDto, TimeTracker>()
				.ForMember(d => d.Id, map => map.Ignore())
				.ForMember(d => d.ProjectId, map => map.Ignore())
				.ForMember(d => d.Project, map => map.Ignore())
				.ForMember(d => d.EmployeeId, map => map.Ignore())
				.ForMember(d => d.Employee, map => map.Ignore())
				.ForMember(d => d.Date, map => map.Ignore());
		}
	}
}
