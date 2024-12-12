using AutoMapper;
using BS.Contracts.Communication;
using PersonalAccountV2.Models.Communication;

namespace PersonalAccountV2.Mapping
{

    public class CommunicationMappingsProfile : Profile
    {
        public CommunicationMappingsProfile()
        {
            CreateMap<CommunicationDto, CommunicationModel>();
            CreateMap<CreatingOrUpdatingCommunicationModel, CreatingOrUpdatingCommunicationDto>();
        }
    }
}
