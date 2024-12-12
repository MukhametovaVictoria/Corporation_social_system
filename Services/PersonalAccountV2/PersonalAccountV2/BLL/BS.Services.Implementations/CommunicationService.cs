using AutoMapper;
using BS.Contracts.Communication;
using BS.Services.Abstractions;
using DA.Entities;
using DA.Repositories.Abstractions;

namespace BS.Services.Implementations
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IMapper _mapper;
        private readonly ICommunicationRepository _communicationRepository;

        public CommunicationService(IMapper mapper, ICommunicationRepository communicationRepository)
        {
            _mapper = mapper;
            _communicationRepository = communicationRepository;
        }

        public async Task<Guid> CreateOrUpdate(CreatingOrUpdatingCommunicationDto communicationEmployee)
        {
            var item = _mapper.Map<CreatingOrUpdatingCommunicationDto, Communication>(communicationEmployee);
            var id = await _communicationRepository.CreateOrUpdate(item);
            await _communicationRepository.SaveChangesAsync();
            return id;
        }

        public async Task<ICollection<CommunicationDto>> GetAllCommunicationEmployee(Guid employee)
        {
            var allCommunication = await _communicationRepository.GetAllCommunicationEmployee(employee);
            return _mapper.Map<ICollection<Communication>, ICollection<CommunicationDto>>(allCommunication);
        }

        public async  Task<CommunicationDto> GetByIdAsync(Guid id)
        {
            var communication = await _communicationRepository.GetAsync(id);
            return _mapper.Map<CommunicationDto>(communication);
        }
    }
}
