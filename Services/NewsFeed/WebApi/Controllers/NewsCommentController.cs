using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts.NewsComment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.NewsComment;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsCommentController : ControllerBase
    {
        private readonly INewsCommentService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<NewsCommentController> _logger;

        public NewsCommentController(
            INewsCommentService service,
            ILogger<NewsCommentController> logger,
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
            if (id == Guid.Empty)
            {
                _logger.LogError("NewsCommentController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("NewsCommentController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<NewsCommentModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsCommentController.GetAsync: {ex}"));
            }
        }

        [Route("CreateAsync")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatingNewsCommentModel newsCommentModel)
        {
            if (newsCommentModel == null)
            {
                _logger.LogError("NewsCommentController.CreateAsync: newsCommentModel is null.");
                return BadRequest(GetBadRequestObject("NewsCommentController.CreateAsync: newsCommentModel is null."));
            }

            try
            {
                return Ok(await _service.CreateAsync(_mapper.Map<CreatingNewsCommentDto>(newsCommentModel)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsCommentController.CreateAsync: {ex}"));
            }
        }

        [Route("EditAsync")]
        [HttpPut]
        public async Task<IActionResult> EditAsync(Guid id, UpdatingNewsCommentModel newsCommentModel)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("NewsCommentController.EditAsync: id is empty.");
                return BadRequest(GetBadRequestObject("NewsCommentController.EditAsync: id is empty."));
            }
            if (newsCommentModel == null)
            {
                _logger.LogError("NewsCommentController.EditAsync: newsCommentModel is null.");
                return BadRequest(GetBadRequestObject("NewsCommentController.EditAsync: newsCommentModel is null."));
            }

            try
            {
                await _service.UpdateAsync(id, _mapper.Map<UpdatingNewsCommentModel, UpdatingNewsCommentDto>(newsCommentModel));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsCommentController.EditAsync: {ex}"));
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("NewsCommentController.DeleteAsync: id is empty.");
                return BadRequest(GetBadRequestObject("NewsCommentController.DeleteAsync: id is empty."));
            }

            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsCommentController.DeleteAsync: {ex}"));
            }
        }

        [Route("CheckIsAuthor")]
        [HttpPost]
        public async Task<IActionResult> CheckIsAuthor(Guid employeeId, ICollection<Guid> newsIds)
        {
            if (employeeId == Guid.Empty)
            {
                _logger.LogError("NewsCommentController.CheckIsAuthor: employeeId is empty.");
                return BadRequest(GetBadRequestObject("NewsCommentController.CheckIsAuthor: employeeId is empty."));
            }
            if (newsIds == null || newsIds.Count == 0)
            {
                _logger.LogError("NewsCommentController.CheckIsAuthor: newsIds is null or empty.");
                return BadRequest(GetBadRequestObject("NewsCommentController.CheckIsAuthor: newsIds is null or empty."));
            }

            try
            {
                return Ok(_mapper.Map<List<NewsCommentInfoModel>>(await _service.CheckIsAuthor(employeeId, newsIds)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsCommentController.CheckIsAuthor: {ex}"));
            }
        }

        [Route("GetCollectionByNewsId")]
        [HttpGet]
        public async Task<IActionResult> GetCollectionByNewsId(Guid newsId, int page, int itemsPerPage)
        {
            if (page == 0 || itemsPerPage == 0)
            {
                _logger.LogError("NewsCommentController.GetCollectionByNewsId: page or itemsPerPage is 0.");
                return BadRequest(GetBadRequestObject("NewsCommentController.GetCollectionByNewsId: page or itemsPerPage is 0."));
            }
            if (newsId == Guid.Empty)
            {
                _logger.LogError("NewsCommentController.GetCollectionByNewsId: newsId is empty.");
                return BadRequest(GetBadRequestObject("NewsCommentController.GetCollectionByNewsId: newsId is empty."));
            }

            try
            {
                return Ok(_mapper.Map<List<NewsCommentModel>>(await _service.GetCollectionByNewsId(newsId, page, itemsPerPage)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsCommentController.GetCollectionByNewsId: {ex}"));
            }
        }

        [Route("GetCollectionByEmployeeId")]
        [HttpGet]
        public async Task<IActionResult> GetCollectionByEmployeeId(Guid employeeId, int page, int itemsPerPage)
        {
            if (page == 0 || itemsPerPage == 0)
            {
                _logger.LogError("NewsCommentController.GetCollectionByEmployeeId: page or itemsPerPage is 0.");
                return BadRequest(GetBadRequestObject("NewsCommentController.GetCollectionByEmployeeId: page or itemsPerPage is 0."));
            }
            if (employeeId == Guid.Empty)
            {
                _logger.LogError("NewsCommentController.GetCollectionByEmployeeId: employeeId is empty.");
                return BadRequest(GetBadRequestObject("NewsCommentController.GetCollectionByEmployeeId: employeeId is empty."));
            }

            try
            {
                return Ok(_mapper.Map<List<NewsCommentModel>>(await _service.GetCollectionByEmployeeId(employeeId, page, itemsPerPage)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"NewsCommentController.GetCollectionByEmployeeId: {ex}"));
            }
        }

        private object GetBadRequestObject(string error)
        {
            return new { Status = "400", Error = error };
        }
    }
}
