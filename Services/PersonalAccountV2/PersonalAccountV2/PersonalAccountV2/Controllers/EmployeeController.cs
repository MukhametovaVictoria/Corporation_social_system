using AutoMapper;
using BS.Contracts.Employee;
using BS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PersonalAccountV2.Models.Employee;
using System.Text.Json;

namespace PersonalAccountV2.Controllers
{
    [Route("api/[controller]")]
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
                return Ok(_mapper.Map<EmployeeModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.GetAsync: {ex}."));
            }
        }

        [Route("GetListAsync")]
        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {

            try
            {
                return Ok(_mapper.Map<List<EmployeeModel>>(await _service.GetAllEmployee()));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.GetListAsync: {ex}."));
            }
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdatingEmployeeModel model)
        {
            if (model == null) 
            {
                return BadRequest("EmployeeController.Update: object is null.");
            }

            try
            {
                return Ok(await _service.Update(_mapper.Map<UpdatingEmployeeModel, UpdatingEmployeeDto>(model)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.Update: {ex}."));
            }
        }

        [Route("CreateOrUpdate")]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] List<ShortEmployeeModel> employees)
        {
            try
            {
                if (employees == null || employees.Count == 0)
                    return BadRequest("EmployeeController.CreateOrUpdate: employees collection is null.");

                await _service.CreateOrUpdateRange(_mapper.Map<List<ShortEmployeeModel>, List<ShortEmployeeDto>>(employees));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.CreateOrUpdate: {ex}."));
            }
        }

        [Route("CheckIsAdmin")]
        [HttpGet]
        public async Task<IActionResult> CheckIsAdmin(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                _logger.LogError("EmployeeController.CheckIsAdmin: employeeId is empty.");
                return BadRequest(GetBadRequestObject("EmployeeController.CheckIsAdmin: employeeId is empty."));
            }

            try
            {
                var employee = _mapper.Map<EmployeeModel>(await _service.GetByIdAsync(employeeId));
                return Ok(employee != null && employee.IsAdmin);
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.CheckIsAdmin: {ex}."));
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
