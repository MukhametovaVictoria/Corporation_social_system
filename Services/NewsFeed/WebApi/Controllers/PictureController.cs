using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts.Picture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Models.Picture;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<PictureController> _logger;

        public PictureController(
            IPictureService service,
            ILogger<PictureController> logger,
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
                _logger.LogError("PictureController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("PictureController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<PictureModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"PictureController.GetAsync: {ex}."));
            }
        }

        [Route("GetCollectionByNewsIds")]
        [HttpPost]
        public async Task<IActionResult> GetCollectionByNewsIds([FromBody]List<Guid> newsIds)
        {
            if (newsIds == null || newsIds.Count == 0)
            {
                _logger.LogError("PictureController.GetCollectionByNewsIds: list of ids is empty.");
                return BadRequest(GetBadRequestObject("PictureController.GetCollectionByNewsIds: list of ids is empty."));
            }

            try
            {
                return Ok(_mapper.Map<List<PictureModel>>(await _service.GetCollectionByNewsIds(newsIds)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"PictureController.GetCollectionByNewsIds: {ex}"));
            }
        }

        [Route("DeleteAsync")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogError("PictureController.DeleteAsync: Id is empty.");
                return BadRequest(GetBadRequestObject("PictureController.DeleteAsync: id is empty."));
            }
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"PictureController.DeleteAsync: {ex}"));
            }
        }

        private object GetBadRequestObject(string error)
        {
            return new { Status = "400", Error = error };
        }
    }
}
