namespace BS.Contracts.Communication
{
    public class CreatingOrUpdatingCommunicationDto
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
