using BS.Contracts.Accomplishment;
using BS.Contracts.Communication;
using BS.Contracts.Event;
using BS.Contracts.Experience;
using BS.Contracts.Skill;

namespace BS.Contracts.Employee
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Position { get; set; }
        public string? MainEmail { get; set; }
        public string? MainTelephoneNumber { get; set; }
        public string? About {  get; set; }
        public DateTime? Birthdate { get; set; }
        public string? OfficeAddress { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public string? Language {  get; set; }
        public List<AccomplishmentDto>? AccomplishmentsList { get; set; }
        public List<SkillDto>? SkillsList { get; set; }
        public List<CommunicationDto>? CommunicationsList { get; set; }
        public List<ExperienceDto>? ExperienceList { get; set; }
        public List<EventDto>? EventList { get; set; }

    }
}
