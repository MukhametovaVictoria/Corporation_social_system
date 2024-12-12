using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts.News;
using DataAccess.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.LikedNews;
using WebApi.Models.News;
using WebApi.Models.Picture;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<NewsController> _logger;

        public NewsController(
            INewsService service,
            ILogger<NewsController> logger,
            IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                _logger.LogError("NewsController.GetAsync: Id is empty.");
                return BadRequest(GetBadRequestObject("NewsController.GetAsync: Id is empty."));
            }
            try
            {
                return Ok(_mapper.Map<NewsModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.GetAsync: {ex}"));
            }
        }

        [Route("CreateAsync")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatingNewsModel newsModel)
        {
            if (newsModel == null)
            {
                _logger.LogError("NewsController.CreateAsync: object is null.");
                return BadRequest(GetBadRequestObject("NewsController.CreateAsync: object is null."));
            }
            try
            {
                return Ok(await _service.CreateAsync(_mapper.Map<CreatingNewsDto>(newsModel)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.CreateAsync: {ex}"));
            }
        }

        [Route("Publish")]
        [HttpPut]
        public async Task<IActionResult> Publish(Guid newsId)
        {
            if (newsId == Guid.Empty)
                return BadRequest(GetBadRequestObject("NewsController.Publish: id is empty."));

            try
            {
                await _service.ChangeVisibility(newsId, true, false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.Publish: {ex}"));
            }
        }

        [Route("Cancel")]
        [HttpPut]
        public async Task<IActionResult> Cancel(Guid newsId)
        {
            if (newsId == Guid.Empty)
                return BadRequest(GetBadRequestObject("NewsController.Cancel: id is empty."));

            try
            {
                await _service.ChangeVisibility(newsId, false, true);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.Cancel: {ex}"));
            }
        }

        [Route("Archive")]
        [HttpPut]
        public async Task<IActionResult> Archive(Guid newsId)
        {
            if (newsId == Guid.Empty)
                return BadRequest(GetBadRequestObject("NewsController.Archive: id is empty."));
            try
            {
                await _service.ChangeVisibility(newsId, true, true);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.Archive: {ex}"));
            }
        }

        [Route("SendOnModeration")]
        [HttpPut]
        public async Task<IActionResult> SendOnModeration(UpdatingNewsModel newsModel)
        {
            if (!CheckParams(newsModel.Id, newsModel, "NewsController.SendOnModeration"))
                return BadRequest(GetBadRequestObject("NewsController.SendOnModeration: id is empty or object is null."));
            try
            {
                var newsDto = _mapper.Map<UpdatingNewsModel, UpdatingNewsDto>(newsModel);
                newsDto.IsPublished = false;
                newsDto.IsArchived = false;

                await _service.UpdateAsync(newsDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.SendOnModeration: {ex}"));
            }
        }

        [Route("DeleteAsync")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("NewsController.DeleteAsync: Id is empty.");
                return BadRequest(GetBadRequestObject("NewsController.DeleteAsync: id is empty."));
            }
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.DeleteAsync: {ex}"));
            }
        }

        [Route("GetPublishedListAsync")]
        [HttpGet]
        public async Task<IActionResult> GetPublishedListAsync(int page, int itemsPerPage)
        {
            if (!CheckParams(page, itemsPerPage, "NewsController.GetPublishedListAsync"))
                return BadRequest(GetBadRequestObject("NewsController.GetPublishedListAsync: page or itemsPerPage is 0."));
            try
            {
                var newsSearch = new NewsSearch() { Skip = (page - 1) * itemsPerPage, Take = itemsPerPage, IsPublished = true, IsArchived = false };
                return Ok(_mapper.Map<List<NewsModel>>(await _service.GetCollection(newsSearch)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.GetPublishedListAsync: {ex}"));
            }
        }

        [Route("GetOnModerationListAsync")]
        [HttpGet]
        public async Task<IActionResult> GetOnModerationListAsync(int page, int itemsPerPage)
        {
            if (!CheckParams(page, itemsPerPage, "NewsController.GetOnModerationListAsync"))
                return BadRequest(GetBadRequestObject("NewsController.GetOnModerationListAsync: page or itemsPerPage is 0."));
            try
            {
                var newsSearch = new NewsSearch() { Skip = (page - 1) * itemsPerPage, Take = itemsPerPage, IsPublished = false, IsArchived = false };
                return Ok(_mapper.Map<List<NewsModel>>(await _service.GetCollection(newsSearch)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.GetOnModerationListAsync: {ex}"));
            }
        }

        [Route("GetArchivedListAsync")]
        [HttpGet]
        public async Task<IActionResult> GetArchivedListAsync(int page, int itemsPerPage, Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                _logger.LogError("NewsController.GetArchivedListAsync: authorId is empty.");
                return BadRequest(GetBadRequestObject("NewsController.GetArchivedListAsync: authorId is empty."));
            }
            
            if (!CheckParams(page, itemsPerPage, "NewsController.GetArchivedListAsync"))
                return BadRequest(GetBadRequestObject("NewsController.GetArchivedListAsync: page or itemsPerPage is 0."));

            try
            {
                var newsSearch = new NewsSearch() { Skip = (page - 1) * itemsPerPage, Take = itemsPerPage, IsPublished = true, IsArchived = true, AuthorId = authorId };
                return Ok(_mapper.Map<List<NewsModel>>(await _service.GetCollection(newsSearch)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.GetArchivedListAsync: {ex}"));
            }
        }

        [Route("GetCancelledListAsync")]
        [HttpGet]
        public async Task<IActionResult> GetCancelledListAsync(int page, int itemsPerPage, Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                _logger.LogError("NewsController.GetArchivedListAsync: authorId is empty.");
                return BadRequest(GetBadRequestObject("NewsController.GetArchivedListAsync: authorId is empty."));
            }
            
            if (!CheckParams(page, itemsPerPage, "NewsController.GetCancelledListAsync"))
                return BadRequest(GetBadRequestObject("NewsController.GetCancelledListAsync: page or itemsPerPage is 0."));

            try
            {
                var newsSearch = new NewsSearch() { Skip = (page - 1) * itemsPerPage, Take = itemsPerPage, IsPublished = false, IsArchived = true, AuthorId = authorId };
                return Ok(_mapper.Map<List<NewsModel>>(await _service.GetCollection(newsSearch)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.GetCancelledListAsync: {ex}"));
            }
        }

        [Route("CheckIsAuthor")]
        [HttpGet]
        public async Task<IActionResult> CheckIsAuthor(Guid newsId, Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                _logger.LogError("NewsController.CheckIsAuthor: authorId is empty.");
                return BadRequest(GetBadRequestObject("NewsController.CheckIsAuthor: authorId is empty."));
            }
            if (newsId == Guid.Empty)
            {
                _logger.LogError("NewsController.CheckIsAuthor: newsId is empty.");
                return BadRequest(GetBadRequestObject("NewsController.CheckIsAuthor: newsId is empty."));
            }

            try
            {
                var news = _mapper.Map<NewsModel>(await _service.GetByIdAsync(newsId));
                return Ok(news != null && news.AuthorId == authorId);
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.CheckIsAuthor: {ex}"));
            }
        }

        [Route("Like")]
        [HttpPost]
        public async Task<IActionResult> Like(GetLikesClass data)
        {
            if (data.CurrentEmployeeId == Guid.Empty)
            {
                _logger.LogError("NewsController.Like: employeeId is empty.");
                return BadRequest(GetBadRequestObject("NewsController.Like: employeeId is empty."));
            }
            if (data.NewsIds == null || data.NewsIds.Count == 0)
            {
                _logger.LogError("NewsController.Like: newsId is empty.");
                return BadRequest(GetBadRequestObject("NewsController.Like: newsId is empty."));
            }

            try
            {
                return Ok(_mapper.Map<LikedNewsInfoModel>(await _service.Like(data.NewsIds[0], data.CurrentEmployeeId)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.Like: {ex}"));
            }
        }

        [Route("GetLikes")]
        [HttpPost]
        public async Task<IActionResult> GetLikes(GetLikesClass data)
        {
            if (data.CurrentEmployeeId == Guid.Empty)
            {
                _logger.LogError("NewsController.GetLikes: employeeId is empty.");
                return BadRequest(GetBadRequestObject("NewsController.GetLikes: employeeId is empty."));
            }
            if (data.NewsIds == null || data.NewsIds.Count == 0)
            {
                _logger.LogError("NewsController.GetLikes: newsIds is null or empty.");
                return BadRequest(GetBadRequestObject("NewsController.GetLikes: newsIds is null or empty."));
            }

            try
            {
                return Ok(_mapper.Map<List<LikedNewsInfoModel>>(await _service.GetLikes(data.NewsIds, data.CurrentEmployeeId)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.GetLikes: {ex}"));
            }
        }

        [Route("GetLikedNewsByEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetLikedNewsByEmployee(Guid employeeId, int page, int itemsPerPage)
        {
            if (employeeId == Guid.Empty)
            {
                _logger.LogError("NewsController.GetLikedNewsByEmployee: employeeId is empty.");
                return BadRequest(GetBadRequestObject("NewsController.GetLikedNewsByEmployee: employeeId is empty."));
            }
            if (!CheckParams(page, itemsPerPage, "NewsController.GetLikedNewsByEmployee"))
                return BadRequest(GetBadRequestObject("NewsController.GetLikedNewsByEmployee: page or itemsPerPage is 0."));

            try
            {
                return Ok(_mapper.Map<List<LikedNewsInfoModel>>(await _service.GetLikedNewsByEmployee(employeeId, page, itemsPerPage)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsController.GetLikedNewsByEmployee: {ex}"));
            }
        }

        private bool CheckParams(Guid id, object newsModel, string methodName)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError($"{methodName}: Id is empty.");
                return false;
            }
            if (newsModel == null)
            {
                _logger.LogError($"{methodName}: object is null.");
                return false;
            }
            
            return true;
        }

        private bool CheckParams(int page, int itemsPerPage, string methodName)
        {
            if (page == 0)
            {
                _logger.LogError($"{methodName}: page number can't be 0.");
                return false;
            }
            if (itemsPerPage == 0)
            {
                _logger.LogError($"{methodName}: items per page number can't be 0.");
                return false;
            }
            
            return true;
        }

        private object GetBadRequestObject(string error)
        {
            return new { Status = "400", Error = error};
        }
    }
}
