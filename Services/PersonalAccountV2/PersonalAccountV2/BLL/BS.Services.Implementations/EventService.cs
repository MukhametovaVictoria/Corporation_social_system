using AutoMapper;
using BS.Contracts.Event;
using BS.Services.Abstractions;
using DA.Entities;
using DA.Repositories.Abstractions;

namespace BS.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public EventService(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<Guid> CreateOrUpdate(CreatingOrUpdatingEventDto eventEmployee)
        {
            var item = _mapper.Map<CreatingOrUpdatingEventDto, Event>(eventEmployee);
            var id = await _eventRepository.CreateOrUpdate(item);
            await _eventRepository.SaveChangesAsync();
            return id;
        }

        public async Task<ICollection<EventDto>> GetAllEventEmployee(Guid employee)
        {
            var allEvent = await _eventRepository.GetAllEventEmployee(employee);
            return _mapper.Map<ICollection<Event>, ICollection<EventDto>>(allEvent);
        }

        public async Task<EventDto> GetByIdAsync(Guid id)
        {
            var entity = await _eventRepository.GetAsync(id);
            return _mapper.Map<EventDto>(entity);
        }
    }
}
