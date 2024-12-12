using FrontEnd.Models.Picture;
using FrontEnd.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FrontEnd.Models
{
    public class NewsModelFacade
    {
        private NewsViewModel newsViewModel;
        private NewsModelPreparer _newsModelPreparer;
        private NewsViewModelPreparer _newsViewModelPreparer;
        private Guid _userId;
        public NewsModelFacade(NewsModelPreparer newsModelPreparer, NewsViewModelPreparer newsViewModelPreparer, Guid userId)
        {
            newsViewModel = new NewsViewModel();
            _newsModelPreparer = newsModelPreparer;
            _newsViewModelPreparer = newsViewModelPreparer;
            _userId = userId;
        }
        
        public async Task PrepareBase()
        {
            _newsModelPreparer.SetBaseFields(newsViewModel);
            await _newsModelPreparer.SetAuthor(newsViewModel);
            await _newsModelPreparer.SetHashtagNews(newsViewModel);
            _newsModelPreparer.SetPictures(newsViewModel);
        }

        public async Task PrepareAdditional()
        {
            _newsViewModelPreparer.SetIsAuthor(newsViewModel, _userId);
            _newsViewModelPreparer.SetAuthorFullName(newsViewModel);
            await _newsViewModelPreparer.SetHashtags(newsViewModel);
            _newsViewModelPreparer.SetLikes(newsViewModel);
        }

        public NewsViewModel GetResult()
        {
            return newsViewModel;
        }
    }
}
