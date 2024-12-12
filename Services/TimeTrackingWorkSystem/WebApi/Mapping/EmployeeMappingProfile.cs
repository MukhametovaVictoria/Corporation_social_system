using AutoMapper;
using Services.Contracts.Employee;
using WebApi.Models;

namespace Services.Implementations.Mapping
{
    public class EmployeeMappingProfile : Profile
	{
		public EmployeeMappingProfile()
		{
			CreateMap<ShortEmployeeDto, ShortEmployeeModel>();
            CreateMap<ShortEmployeeModel, ShortEmployeeDto>();
        }
	}
}
