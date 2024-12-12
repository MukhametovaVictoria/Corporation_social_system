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
                .ForMember(d => d.Birthdate, map => map.Ignore())
                .ForMember(d => d.OfficeAddress, map => map.Ignore())
                .ForMember(d => d.MainTelephoneNumber, map => map.Ignore())
                .ForMember(d => d.EmploymentDate, map => map.Ignore())
                .ForMember(d => d.About, map => map.Ignore())
                .ForMember(d => d.MainEmail, map => map.Ignore());

            CreateMap<UpdatingEmployeeDto, Employee>();

            CreateMap<CreatingEmployeeDto, Employee>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.IsDeleted, map => map.Ignore());
        }
    }
}
