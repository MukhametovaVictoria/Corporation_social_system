using AutoMapper;
using BS.Contracts.Skill;
using BS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PersonalAccountV2.Models.Skill;

namespace PersonalAccountV2.Controllers
{
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<SkillController> _logger;

        public SkillController(
            ISkillService service,
            ILogger<SkillController> logger,
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
                _logger.LogError("SkillController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("SkillController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<SkillModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"SkillController.GetAsync: {ex}."));
            }
        }

        [Route("GetAllSkillEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetAllSkillEmployee(Guid employeeId)
        {

            try
            {
                return Ok(_mapper.Map<List<SkillModel>>(await _service.GetAllSkillEmployee(employeeId)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"SkillController.GetAllSkillEmployee: {ex}."));
            }
        }

        [Route("CreateOrUpdate")]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreatingOrUpdatingSkillModel skill)
        {
            try
            {
                if (skill == null)
                    return BadRequest("SkillController.CreateOrUpdate: SkillEmployee is null.");

                return Ok(await _service.CreateOrUpdate(_mapper.Map<CreatingOrUpdatingSkillModel, CreatingOrUpdatingSkillDto>(skill)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"SkillController.CreateOrUpdate: {ex}."));
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
