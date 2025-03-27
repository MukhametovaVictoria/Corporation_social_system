namespace Domain.Entities
{
    public class Leave : Entity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int Count => (EndDate - StartDate).Days + 1;
        public bool IsPaid { get; set; }
    }
}