using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Project;
using WebApi.Models.Project;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProjectController : ControllerBase
	{
		private readonly IProjectService _service;
		private readonly IMapper _mapper;
		private readonly ILogger<ProjectController> _logger;

		public ProjectController(IProjectService service, ILogger<ProjectController> logger, IMapper mapper)
		{
			_service = service;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			var projects = await _service.GetAllAsync(new CancellationToken());
			return Ok(_mapper.Map<List<ProjectModel>>(projects));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(Guid id)
		{
			return Ok(_mapper.Map<ProjectModel>(await _service.GetByIdAsync(id)));
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateAsync([FromBody]CreatingProjectModel model)
		{
			return Ok(await _service.CreateAsync(_mapper.Map<CreatingProjectDto>(model)));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> EditAsync(Guid id, UpdatingProjectModel model)
		{
			await _service.UpdateAsync(id, _mapper.Map<UpdatingProjectModel, UpdatingProjectDto>(model));
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			await _service.DeleteAsync(id);
			return Ok();
		}

		[HttpPost("list")]
		public async Task<IActionResult> GetListAsync(ProjectFilterModel filterModel)
		{
			var filterDto = _mapper.Map<ProjectFilterModel, ProjectFilterDto>(filterModel);
			return Ok(_mapper.Map<List<ProjectModel>>(await _service.GetPagedAsync(filterDto)));
		}
	}
}