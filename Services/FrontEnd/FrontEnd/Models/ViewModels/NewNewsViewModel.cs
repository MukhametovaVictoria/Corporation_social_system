namespace FrontEnd.Models.ViewModels
{
    public class NewNewsViewModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public IEnumerable<IFormFile> Pictures { get; set; }
        public string Hashtags { get; set; }
    }
}
