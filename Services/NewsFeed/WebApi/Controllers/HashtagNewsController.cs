using AutoMapper;
using BusinessLogic.Contracts.HashtagNews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using WebApi.Models.HashtagNews;
using BusinessLogic.Contracts.Hashtag;
using WebApi.Models.Hashtag;
using System.Collections.Generic;
using BusinessLogic.Abstractions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HashtagNewsController : ControllerBase
    {
        private readonly IHashtagNewsService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<HashtagNewsController> _logger;

        public HashtagNewsController(
            IHashtagNewsService service,
            ILogger<HashtagNewsController> logger,
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
                _logger.LogError("HashtagNewsController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("HashtagNewsController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<HashtagNewsModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HashtagNewsController.GetAsync: {ex}."));
            }
        }

        [Route("CreateAsync")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatingHashtagNewsModel hashtagNewsModel)
        {
            if (hashtagNewsModel == null)
            {
                _logger.LogError("HashtagNewsController.CreateAsync: hashtagNewsModel is null.");
                return BadRequest(GetBadRequestObject("HashtagNewsController.CreateAsync: hashtagNewsModel is null."));
            }

            try
            {
                return Ok(await _service.CreateAsync(_mapper.Map<CreatingHashtagNewsDto>(hashtagNewsModel)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HashtagNewsController.CreateAsync: {ex}"));
            }
        }

        [Route("GetCollectionByNewsIds")]
        [HttpPost]
        public async Task<IActionResult> GetCollectionByNewsIds(List<Guid> newsIds)
        {
            if (newsIds == null || newsIds.Count == 0)
            {
                _logger.LogError("HashtagNewsController.GetCollectionByNewsIds: ids is empty.");
                return BadRequest(GetBadRequestObject("HashtagNewsController.GetCollectionByNewsIds: ids is empty."));
            }

            try
            {
                return Ok(_mapper.Map<List<HashtagNewsModel>>(await _service.GetCollectionByNewsIds(newsIds)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HashtagNewsController.GetCollectionByNewsIds: {ex}"));
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("HashtagNewsController.DeleteAsync: id is empty.");
                return BadRequest(GetBadRequestObject("HashtagNewsController.DeleteAsync: id is empty."));
            }

            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"HashtagNewsController.DeleteAsync: {ex}."));
            }
        }

        private object GetBadRequestObject(string error)
        {
            return new { Status = "400", Error = error };
        }
    }
}
