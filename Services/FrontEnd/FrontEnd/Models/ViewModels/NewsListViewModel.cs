namespace FrontEnd.Models
{
    public class NewsListViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public Dictionary<string, object> Filters { get; set; }
        public bool FiltersEmpty { get; set; }
    }
}
