using AutoMapper;
using BS.Contracts.Communication;
using BS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PersonalAccountV2.Models.Communication;

namespace PersonalAccountV2.Controllers
{
    [Route("api/[controller]")]
    public class CommunicationController : ControllerBase
    {
        private readonly ICommunicationService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<CommunicationController> _logger;

        public CommunicationController(
            ICommunicationService service,
            ILogger<CommunicationController> logger,
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
                _logger.LogError("CommunicationController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("CommunicationController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<CommunicationModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"CommunicationController.GetAsync: {ex}."));
            }
        }

        [Route("GetAllCommunicationEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetAllCommunicationEmployee(Guid employeeId)
        {

            try
            {
                return Ok(_mapper.Map<List<CommunicationModel>>(await _service.GetAllCommunicationEmployee(employeeId)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"CommunicationController.GetAllCommunicationEmployee: {ex}."));
            }
        }

        [Route("CreateOrUpdate")]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreatingOrUpdatingCommunicationModel communication)
        {
            try
            {
                if (communication == null)
                    return BadRequest("CommunicationController.CreateOrUpdate: CommunicationEmployee is null.");

                return Ok(await _service.CreateOrUpdate(_mapper.Map<CreatingOrUpdatingCommunicationModel, CreatingOrUpdatingCommunicationDto>(communication)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"CommunicationController.CreateOrUpdate: {ex}."));
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
