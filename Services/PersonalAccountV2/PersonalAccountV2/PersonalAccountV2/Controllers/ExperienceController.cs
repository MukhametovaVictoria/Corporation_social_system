using AutoMapper;
using BS.Contracts.Experience;
using BS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PersonalAccountV2.Models.Experience;

namespace PersonalAccountV2.Controllers
{
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ExperienceController> _logger;

        public ExperienceController(
            IExperienceService service,
            ILogger<ExperienceController> logger,
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
                _logger.LogError("ExperienceController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("ExperienceController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<ExperienceModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"ExperienceController.GetAsync: {ex}."));
            }
        }

        [Route("GetAllExperienceEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetAllExperienceEmployee(Guid employeeId)
        {

            try
            {
                return Ok(_mapper.Map<List<ExperienceModel>>(await _service.GetAllExperienceEmployee(employeeId)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"ExperienceController.GetAllExperienceEmployee: {ex}."));
            }
        }

        [Route("CreateOrUpdate")]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreatingOrUpdatingExperienceModel experience)
        {
            try
            {
                if (experience == null)
                    return BadRequest("ExperienceController.CreateOrUpdate: ExperienceEmployee is null.");

                return Ok(await _service.CreateOrUpdate(_mapper.Map<CreatingOrUpdatingExperienceModel, CreatingOrUpdatingExperienceDto>(experience)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"ExperienceController.CreateOrUpdate: {ex}."));
            }
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
            return new { Status = "400", Error = error };
        }
    }
}
