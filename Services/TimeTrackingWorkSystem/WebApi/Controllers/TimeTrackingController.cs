using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.TimeTracker;
using WebApi.Models.TimeTracking;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TimeTrackerController : ControllerBase
	{
		private readonly ITimeTrackerService _service;
		private readonly IMapper _mapper;
		private readonly ILogger<TimeTrackerController> _logger;

		public TimeTrackerController(ITimeTrackerService service, ILogger<TimeTrackerController> logger, IMapper mapper)
		{
			_service = service;
			_logger = logger;
			_mapper = mapper;
		}

		[Route("all")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var timeTrackers = await _service.GetAllAsync(new CancellationToken());
			return Ok(_mapper.Map<List<TimeTrackerModel>>(timeTrackers));
		}

        [Route("{id}")]
        [HttpGet]
		public async Task<IActionResult> GetAsync(Guid id)
		{
			return Ok(_mapper.Map<TimeTrackerModel>(await _service.GetByIdAsync(id)));
		}

        [Route("CreateAsync")]
        [HttpPost]
		public async Task<IActionResult> CreateAsync(CreatingTimeTrackerModel timeTrackingModel)
		{
			return Ok(await _service.CreateAsync(_mapper.Map<CreatingTimeTrackerDto>(timeTrackingModel)));
		}

        [Route("CreateRangeAsync")]
        [HttpPost]
		public async Task<IActionResult> CreateRangeAsync([FromBody] List<CreatingTimeTrackerModel> timeTrackingModel)
		{
			var list = await _service.CreateRangeAsync(_mapper.Map<List<CreatingTimeTrackerDto>>(timeTrackingModel));
			return Ok(_mapper.Map<List<TimeTrackerModel>>(list));
		}

        [Route("DeleteRangeAsync")]
        [HttpPost]
		public async Task<IActionResult> DeleteRangeAsync([FromBody] List<Guid> ids)
		{
			await _service.DeleteRangeAsync(ids);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> EditAsync(Guid id, [FromBody] UpdatingTimeTrackerModel timeTrackingModel)
		{
			await _service.UpdateAsync(id, _mapper.Map<UpdatingTimeTrackerModel, UpdatingTimeTrackerDto>(timeTrackingModel));
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			await _service.DeleteAsync(id);
			return Ok();
		}

        [Route("list")]
        [HttpPost]
		public async Task<IActionResult> GetListAsync([FromBody] TimeTrackerFilterModel filterModel)
		{
			var filterDto = _mapper.Map<TimeTrackerFilterModel, TimeTrackerFilterDto>(filterModel);
			return Ok(_mapper.Map<List<TimeTrackerModel>>(await _service.GetPagedAsync(filterDto)));
		}
	}
}