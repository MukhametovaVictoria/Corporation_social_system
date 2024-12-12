using AutoMapper;
using BS.Contracts.Event;
using BS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PersonalAccountV2.Models.Event;

namespace PersonalAccountV2.Controllers
{
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<EventController> _logger;

        public EventController(
            IEventService service,
            ILogger<EventController> logger,
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
                _logger.LogError("EventController.GetAsync: id is empty.");
                return BadRequest(GetBadRequestObject("EventController.GetAsync: id is empty."));
            }

            try
            {
                return Ok(_mapper.Map<EventModel>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EventController.GetAsync: {ex}."));
            }
        }

        [Route("GetAllEventEmployee")]
        [HttpGet]
        public async Task<IActionResult> GetAllEventEmployee(Guid employeeId)
        {

            try
            {
                return Ok(_mapper.Map<List<EventModel>>(await _service.GetAllEventEmployee(employeeId)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EventController.GetAllEventEmployee: {ex}."));
            }
        }

        [Route("CreateOrUpdate")]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreatingOrUpdatingEventModel eventEntity)
        {
            try
            {
                if (eventEntity == null)
                    return BadRequest("EventController.CreateOrUpdate: EventEmployee is null.");

                return Ok(await _service.CreateOrUpdate(_mapper.Map<CreatingOrUpdatingEventModel, CreatingOrUpdatingEventDto>(eventEntity)));
            }
            catch (Exception ex)
            {
                return BadRequest(GetBadRequestObject($"EventController.CreateOrUpdate: {ex}."));
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
