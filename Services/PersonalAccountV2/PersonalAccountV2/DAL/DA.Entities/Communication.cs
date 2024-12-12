namespace DA.Entities
{
    public class Communication : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public virtual Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
