namespace DA.Entities
{
    public class Event : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAcrive { get; set; }
        public virtual Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
