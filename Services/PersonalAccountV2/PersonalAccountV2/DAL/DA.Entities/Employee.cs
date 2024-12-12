namespace DA.Entities
{
    public class Employee : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Position { get; set; }
        public string? MainEmail { get; set; }
        public string? MainTelephoneNumber { get; set; }
        public string? About { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? OfficeAddress { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public string? Language {  get; set; }
        public ICollection<Accomplishment>? AccomplishmentsList { get; set; }
        public ICollection<Skill>? SkillsList { get; set; }
        public ICollection<Communication>? CommunicationsList { get; set; }
        public ICollection<Experience>? ExperienceList { get; set; }
        public ICollection<Event>? EventList { get; set; }
    }



}
