namespace FrontEnd.Models
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Position { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public List<NewsModel>? NewsList { get; set; }
        public List<NewsCommentModel>? NewsCommentList { get; set; }
    }
}
