namespace DA.Entities
{
    public class Experience : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Company { get; set; }
        public DateTime EmployementDate { get; set; }
        public DateTime DismissalDate { get; set; }
        public string DescriptionWork { get; set; }
        public string DescriptionCompany { get; set; }
        public virtual Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
