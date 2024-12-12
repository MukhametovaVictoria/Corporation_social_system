using FrontEnd.Models.Picture;
using FrontEnd.Services;

namespace FrontEnd.Models
{
    public class NewsViewModelPreparer
    {
        private NewsService _newsServise;
        private NewsModel _newsItem;
        private List<LikedNewsInfoModel> _likes = new List<LikedNewsInfoModel>();

        public NewsViewModelPreparer SetService(NewsService newsServise)
        {
            _newsServise = newsServise;
            return this;
        }

        public NewsViewModelPreparer SetNews(NewsModel newsItem)
        {
            _newsItem = newsItem;
            return this;
        }

        public NewsViewModelPreparer SetLikesList(List<LikedNewsInfoModel> likes)
        {
            _likes = likes;
            return this;
        }

        public void SetIsAuthor(NewsViewModel newsViewModel, Guid currentUserId)
        {
            newsViewModel.IsAuthor = _newsItem?.AuthorId == currentUserId;
        }

        public void SetAuthorFullName(NewsViewModel newsViewModel)
        {
            if(_newsItem?.Author != null)
            {
                newsViewModel.AuthorFullName = _newsItem?.Author?.Firstname + " " + _newsItem?.Author?.Surname;
            }
            else if (newsViewModel?.Author != null)
            {
                newsViewModel.AuthorFullName = newsViewModel.Author.Firstname + " " + newsViewModel.Author.Surname;
            }
        }

        public async Task SetHashtags(NewsViewModel newsViewModel)
        {
            if (_newsServise != null)
            {
                try
                {
                    if (newsViewModel.HashtagNewsList != null)
                    {
                        var hashtagIds = newsViewModel.HashtagNewsList.Select(x => x.HashtagId).ToList();
                        var hashtags = hashtagIds != null && hashtagIds.Count > 0 ? await _newsServise.GetHashtags(hashtagIds) : null;
                        newsViewModel.HashtagList = hashtags;
                        var names = hashtags?.Select(x => "#" + x.Name).ToList();
                        newsViewModel.Hashtags = names != null && names.Count > 0 ? string.Join(" ", names) : "";
                    }
                }
                catch (Exception ex) { }
            }
        }

        public void SetLikes(NewsViewModel newsViewModel)
        {

            var likeInfo = _likes?.FirstOrDefault(x => x.NewsId == _newsItem.Id);
            if (likeInfo != null)
            {
                newsViewModel.IsLikedByCurrentUser = likeInfo.IsLiked;
                newsViewModel.Likes = likeInfo.LikesCount;
            }
        }
    }
}
