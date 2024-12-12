using BS.Contracts.Event;

namespace BS.Services.Abstractions
{
    public interface IEventService
    {
        Task<ICollection<EventDto>> GetAllEventEmployee(Guid employee);

        Task<EventDto> GetByIdAsync(Guid id);

        public Task<Guid> CreateOrUpdate(CreatingOrUpdatingEventDto eventEmployee);
    }
}
