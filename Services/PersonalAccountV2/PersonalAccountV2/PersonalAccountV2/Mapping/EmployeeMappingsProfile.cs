using AutoMapper;
using BS.Contracts.Employee;
using PersonalAccountV2.Models.Employee;

namespace PersonalAccountV2.Mapping
{
    public class EmployeeMappingsProfile : Profile
    {
        public EmployeeMappingsProfile()
        {
            CreateMap<EmployeeDto, EmployeeModel>();
            CreateMap<ShortEmployeeModel, ShortEmployeeDto>();
            CreateMap<EmployeeFilterModel, EmployeeFilterDto>();
            CreateMap<UpdatingEmployeeModel, UpdatingEmployeeDto>();
        }
    }
}
