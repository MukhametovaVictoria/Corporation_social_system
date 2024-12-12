namespace PersonalAccountV2.Models.Employee
{
    public class ShortEmployeeModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Position { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
    }
}
