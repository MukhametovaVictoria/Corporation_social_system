using AutoMapper;
using BS.Contracts.Communication;
using DA.Entities;

namespace BS.Services.Implementations.Mapping
{

    public class CommunicationMappingsProfile : Profile
    {
        public CommunicationMappingsProfile()
        {
            CreateMap<Communication, CommunicationDto>();

            CreateMap<CreatingOrUpdatingCommunicationDto, Communication>()
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.Employee, map => map.Ignore());
        }
    }
}
