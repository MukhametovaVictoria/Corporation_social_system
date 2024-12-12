namespace BS.Contracts.Accomplishment
{
    public class AccomplishmentFilterDto
    {
        public int Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
