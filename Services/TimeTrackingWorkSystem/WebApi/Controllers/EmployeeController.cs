using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Employee;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            IEmployeeService service,
            ILogger<EmployeeController> logger,
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
                _logger.LogError("EmployeeController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("EmployeeController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<ShortEmployeeModel>(await _service.GetByIdAsync(id, new CancellationToken())));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.GetAsync: {ex}."));
            }
        }

        [Route("GetListAsync")]
        [HttpGet]
        public async Task<IActionResult> GetListAsync(int page, int itemsPerPage)
        {
            if (!CheckParams(page, itemsPerPage, "EmployeeController.GetListAsync: page or itemsPerPage is 0."))
                return BadRequest("EmployeeController.GetListAsync: page or itemsPerPage is 0.");

            try
            {
                return Ok(_mapper.Map<List<ShortEmployeeModel>>(await _service.GetPagedAsync(page, itemsPerPage)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.GetListAsync: {ex}."));
            }
        }

        [Route("CreateOrUpdateEmployeeRange")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrUpdateEmployeeRange([FromBody] List<ShortEmployeeModel> employees)
        {
            try
            {
                if (employees == null)
                    return BadRequest("EmployeeController.CreateOrUpdateEmployeeRange: employees collection is null.");

                if (employees != null && employees.Count > 0)
                    return Ok(await _service.CreateOrUpdateRange(_mapper.Map<List<ShortEmployeeModel>, List<ShortEmployeeDto>>(employees)));

                return Ok(false);
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.CreateOrUpdateEmployeeRange: {ex}."));
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
