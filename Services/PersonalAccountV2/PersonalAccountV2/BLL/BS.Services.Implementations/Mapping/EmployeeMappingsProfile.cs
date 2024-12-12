using AutoMapper;
using BS.Contracts.Employee;
using DA.Entities;

namespace BS.Services.Implementations.Mapping
{
    public class EmployeeMappingsProfile : Profile
    {
        public EmployeeMappingsProfile()
        {
            CreateMap<Employee, EmployeeDto>();

            CreateMap<UpdatingEmployeeDto, Employee>()
                .ForMember(d => d.SkillsList, map => map.Ignore())
                .ForMember(d => d.EventList, map => map.Ignore())
                .ForMember(d => d.AccomplishmentsList, map => map.Ignore())
                .ForMember(d => d.CommunicationsList, map => map.Ignore())
                .ForMember(d => d.ExperienceList, map => map.Ignore())
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore());

            CreateMap<ShortEmployeeDto, Employee>()
                .ForMember(d => d.SkillsList, map => map.Ignore())
                .ForMember(d => d.EventList, map => map.Ignore())
                .ForMember(d => d.AccomplishmentsList, map => map.Ignore())
                .ForMember(d => d.CommunicationsList, map => map.Ignore())
                .ForMember(d => d.ExperienceList, map => map.Ignore())
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.MainEmail, map => map.Ignore())
                .ForMember(d => d.MainTelephoneNumber, map => map.Ignore())
                .ForMember(d => d.About, map => map.Ignore())
                .ForMember(d => d.Birthdate, map => map.Ignore())
                .ForMember(d => d.OfficeAddress, map => map.Ignore())
                .ForMember(d => d.EmploymentDate, map => map.Ignore())
                .ForMember(d => d.Language, map => map.Ignore());
        }
    }
}
