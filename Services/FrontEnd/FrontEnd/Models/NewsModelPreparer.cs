using FrontEnd.Models.Picture;
using FrontEnd.Services;

namespace FrontEnd.Models
{
    public class NewsModelPreparer
    {
        private NewsService _newsServise;
        private NewsModel _newsItem;
        private List<PictureModel> _pics = new List<PictureModel>();
        private List<EmployeeModel> _employees = new List<EmployeeModel>();

        public NewsModelPreparer SetService(NewsService newsServise)
        {
            _newsServise = newsServise;
            return this;
        }

        public NewsModelPreparer SetPicturesList(List<PictureModel> pics)
        {
            _pics = pics;
            return this;
        }

        public NewsModelPreparer SetEmployeesList(List<EmployeeModel> employees)
        {
            _employees = employees;
            return this;
        }

        public NewsModelPreparer SetNews(NewsModel newsItem)
        {
            _newsItem = newsItem;
            return this;
        }

        public void SetBaseFields(NewsModel newsViewModel)
        {
            if (_newsItem != null)
            {
                newsViewModel.Id = _newsItem.Id;
                newsViewModel.Title = _newsItem.Title;
                newsViewModel.Content = _newsItem.Content;
                newsViewModel.ShortDescription = _newsItem.ShortDescription;
                newsViewModel.CreatedAt = _newsItem.CreatedAt;
                newsViewModel.UpdatedAt = _newsItem.UpdatedAt;
                newsViewModel.AuthorId = _newsItem.AuthorId;
            }
        }

        public async Task SetAuthor(NewsModel newsViewModel)
        {
            if (_employees == null)
                _employees = new List<EmployeeModel>();

            if (_newsItem != null)
            {
                if (_newsItem.Author == null)
                {
                    try
                    {
                        var isInList = _employees.FirstOrDefault(x => x.Id == _newsItem.AuthorId) != null;
                        var employee = isInList ? _employees.FirstOrDefault(x => x.Id == _newsItem.AuthorId) : await _newsServise.GetEmployeeInfo(_newsItem.AuthorId);

                        if (employee != null)
                        {
                            if (!isInList && _employees != null)
                                _employees.Add(employee);

                            newsViewModel.Author = employee;
                        }
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    if (!_employees.Contains(_newsItem.Author))
                        _employees.Add(_newsItem.Author);

                    newsViewModel.Author = _newsItem.Author;
                }
            }
        }

        public async Task SetHashtagNews(NewsModel newsViewModel)
        {
            if (_newsItem != null)
            {
                if (_newsItem.HashtagNewsList != null)
                {
                    newsViewModel.HashtagNewsList = _newsItem.HashtagNewsList;
                }
                else
                {
                    try
                    {
                        var hashtagNews = await _newsServise.GetHashtagNewsByNewsIds(new List<Guid>() { _newsItem.Id });
                        newsViewModel.HashtagNewsList = hashtagNews;
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public void SetPictures(NewsModel newsViewModel)
        {
            if (_newsItem != null)
            {
                if (_newsItem.PictureList != null)
                {
                    newsViewModel.PictureList = _newsItem.PictureList;
                }
                else
                {
                    if (_pics != null)
                        newsViewModel.PictureList = _pics.Where(x => x.NewsId == _newsItem.Id).ToList();
                }
            }
        }
    }
}
