namespace FrontEnd.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsFound { get; set; }
        public bool IsDeleted { get; set; }
    }
}