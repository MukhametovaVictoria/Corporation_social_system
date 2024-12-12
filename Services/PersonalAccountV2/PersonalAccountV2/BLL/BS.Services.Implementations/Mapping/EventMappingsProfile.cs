using AutoMapper;
using BS.Contracts.Event;
using DA.Entities;

namespace BS.Services.Implementations.Mapping
{

    public class EventMappingsProfile : Profile
    {
        public EventMappingsProfile()
        {
            CreateMap<Event, EventDto>();

            CreateMap<CreatingOrUpdatingEventDto, Event>()
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.Employee, map => map.Ignore());
        }
    }
}
