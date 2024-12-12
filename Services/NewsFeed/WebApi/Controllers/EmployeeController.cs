using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Models.Employee;

namespace WebApi.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> GetListAsync(int page, int itemsPerPage)
        {
            if(!CheckParams(page, itemsPerPage, "EmployeeController.GetListAsync: page or itemsPerPage is 0."))
                return BadRequest("EmployeeController.GetListAsync: page or itemsPerPage is 0.");

            try
            {
                return Ok(_mapper.Map<List<EmployeeModel>>(await _service.GetPagedAsync(page, itemsPerPage)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EmployeeController.GetListAsync: {ex}."));
            }
        }

        [Route("CreateOrUpdateEmployeeRange")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateOrUpdateEmployeeRange(List<ShortEmployeeModel> employees)
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
