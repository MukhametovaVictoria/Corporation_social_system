namespace BS.Contracts.Experience
{
    public class CreatingOrUpdatingExperienceDto
    {
        public Guid Id { get; set; }
        public string Company { get; set; }
        public DateTime EmployementDate { get; set; }
        public DateTime? DismissalDate { get; set; }
        public string? DescriptionWork { get; set; }
        public string? DescriptionCompany { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
