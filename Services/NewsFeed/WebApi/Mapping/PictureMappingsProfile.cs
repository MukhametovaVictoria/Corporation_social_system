using AutoMapper;
using BusinessLogic.Contracts.Picture;
using DataAccess.Entities;
using WebApi.Models.Picture;

namespace WebApi.Mapping
{
    public class PictureMappingsProfile : Profile
    {
        public PictureMappingsProfile()
        {
            CreateMap<PictureDto, PictureModel>();

            CreateMap<CreatingPictureModel, CreatingPictureDto>();

            CreateMap<UpdatingPictureModel, UpdatingPictureDto>();
        }
    }
}
