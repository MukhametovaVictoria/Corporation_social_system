using AutoMapper;
using BusinessLogic.Contracts.Employee;
using BusinessLogic.Contracts.Picture;
using DataAccess.Entities;

namespace BusinessLogic.Services.Mapping
{
    public class PictureMappingsProfile : Profile
    {
        public PictureMappingsProfile()
        {
            CreateMap<Picture, PictureDto>();

            CreateMap<CreatingPictureDto, Picture>()
                .ForMember(d => d.Author, map => map.Ignore())
                .ForMember(d => d.News, map => map.Ignore())
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.CreatedAt, map => map.Ignore());

            CreateMap<UpdatingPictureDto, Picture>()
                .ForMember(d => d.Author, map => map.Ignore())
                .ForMember(d => d.News, map => map.Ignore())
                .ForMember(d => d.CreatedAt, map => map.Ignore());
        }
    }
}
