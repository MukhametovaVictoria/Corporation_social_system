using AutoMapper;
using BusinessLogic.Contracts.Employee;
using DataAccess.Entities;

namespace BusinessLogic.Services.Mapping
{
    public class EmployeeMappingsProfile : Profile
    {
        public EmployeeMappingsProfile()
        {
            CreateMap<Employee, EmployeeDto>();

            CreateMap<ShortEmployeeDto, Employee>()
                .ForMember(d => d.NewsCommentList, map => map.Ignore())
                .ForMember(d => d.NewsList, map => map.Ignore())
                .ForMember(d => d.PictureList, map => map.Ignore());
        }
    }
}
