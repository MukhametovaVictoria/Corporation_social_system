namespace BS.Contracts.Event
{
    public class ShortEventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
