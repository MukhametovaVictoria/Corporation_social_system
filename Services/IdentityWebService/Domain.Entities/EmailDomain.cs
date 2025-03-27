namespace Domain.Entities
{
    public class EmailDomain
    {
        public Guid Id { get; set; }
        public required string Domain { get; set; }
    }
}
