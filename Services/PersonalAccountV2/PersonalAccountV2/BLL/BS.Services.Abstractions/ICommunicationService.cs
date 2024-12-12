using BS.Contracts.Communication;

namespace BS.Services.Abstractions
{
    public interface ICommunicationService
    {
        Task<ICollection<CommunicationDto>> GetAllCommunicationEmployee(Guid employee);

        Task<CommunicationDto> GetByIdAsync(Guid id);

        public Task<Guid> CreateOrUpdate(CreatingOrUpdatingCommunicationDto communicationEmployee);
    }
}
