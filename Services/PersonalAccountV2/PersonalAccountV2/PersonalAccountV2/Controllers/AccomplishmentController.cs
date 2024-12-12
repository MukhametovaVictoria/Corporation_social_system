using AutoMapper;
using BS.Contracts.Accomplishment;
using BS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PersonalAccountV2.Models.Accomplishment;

namespace PersonalAccountV2.Controllers
{
    [Route("api/[controller]")]
    public class AccomplishmentController : ControllerBase
    {
        private readonly IAccomplishmentService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<AccomplishmentController> _logger;

        public AccomplishmentController(
            IAccomplishmentService service,
            ILogger<AccomplishmentController> logger,
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
                _logger.LogError("AccomplishmentController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("AccomplishmentController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<AccomplishmentModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"AccomplishmentController.GetAsync: {ex}."));
            }
        }

        [Route("GetAllAccomplishmentEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetAllAccomplishmentEmployee(Guid employeeId)
        {

            try
            {
                return Ok(_mapper.Map<List<AccomplishmentModel>>(await _service.GetAllAccomplishmentEmployee(employeeId)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"AccomplishmentController.GetAllAccomplishmentEmployee: {ex}."));
            }
        }

        [Route("CreateOrUpdate")]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreatingOrUpdatingAccomplishmentModel accomplishment)
        {
            try
            {
                if (accomplishment == null)
                    return BadRequest("AccomplishmentController.CreateOrUpdate: accomplishmentEmployee is null.");

                return Ok(await _service.CreateOrUpdate(_mapper.Map<CreatingOrUpdatingAccomplishmentModel, CreatingOrUpdatingAccomplishmentDto>(accomplishment)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"AccomplishmentController.CreateOrUpdate: {ex}."));
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
