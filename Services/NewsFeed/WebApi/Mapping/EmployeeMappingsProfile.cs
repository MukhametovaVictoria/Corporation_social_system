using AutoMapper;
using BusinessLogic.Contracts.Employee;
using WebApi.Models.Employee;

namespace WebApi.Mapping
{
    public class EmployeeMappingsProfile : Profile
    {
        public EmployeeMappingsProfile()
        {
            CreateMap<EmployeeDto, EmployeeModel>();
            CreateMap<ShortEmployeeModel, ShortEmployeeDto>();
            CreateMap<EmployeeFilterModel, EmployeeFilterDto>();
        }
    }
}
