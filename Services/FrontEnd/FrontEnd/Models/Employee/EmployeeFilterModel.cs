namespace FrontEnd.Models
{
    public class EmployeeFilterModel
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Position { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
