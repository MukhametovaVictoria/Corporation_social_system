namespace BS.Contracts.Accomplishment
{
    public class CreatingOrUpdatingAccomplishmentDto
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
