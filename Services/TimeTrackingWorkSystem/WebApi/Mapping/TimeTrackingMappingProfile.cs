using AutoMapper;
using Services.Contracts.TimeTracker;
using WebApi.Models.TimeTracking;

namespace TimeTrackingWorkSystem.Mapping
{
	public class TimeTrackingMappingProfile : Profile
	{
		public TimeTrackingMappingProfile()
		{
			CreateMap<TimeTrackerDto, TimeTrackerModel>();
			CreateMap<CreatingTimeTrackerModel, CreatingTimeTrackerDto>();
			CreateMap<UpdatingTimeTrackerModel, UpdatingTimeTrackerDto>();
			CreateMap<TimeTrackerFilterModel, TimeTrackerFilterDto>();
		}
	}
}