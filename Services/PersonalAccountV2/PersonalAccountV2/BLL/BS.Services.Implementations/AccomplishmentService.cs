using AutoMapper;
using BS.Contracts.Accomplishment;
using BS.Services.Abstractions;
using DA.Entities;
using DA.Repositories.Abstractions;

namespace BS.Services.Implementations
{
    public class AccomplishmentService : IAccomplishmentService
    {
        private readonly IMapper _mapper;
        private readonly IAccomplishmentRepository _accomplishmentRepository;

        public AccomplishmentService(IMapper mapper, IAccomplishmentRepository accomplishmentRepository)
        {
            _mapper = mapper;
            _accomplishmentRepository = accomplishmentRepository;
        }

        public async Task<Guid> CreateOrUpdate(CreatingOrUpdatingAccomplishmentDto accomplishmentEmployee)
        {
            var item = _mapper.Map<CreatingOrUpdatingAccomplishmentDto, Accomplishment>(accomplishmentEmployee);
            var id = await _accomplishmentRepository.CreateOrUpdate(item);
            await _accomplishmentRepository.SaveChangesAsync();
            return id;
        }

        public async Task<ICollection<AccomplishmentDto>> GetAllAccomplishmentEmployee(Guid employee)
        {
            var allAccomplishment = await _accomplishmentRepository.GetAllAccomplishmentEmployee(employee);
            return _mapper.Map<ICollection<Accomplishment>, ICollection<AccomplishmentDto>>(allAccomplishment);
        }

        public async Task<AccomplishmentDto> GetByIdAsync(Guid id)
        {
            var employee = await _accomplishmentRepository.GetAsync(id);
            return _mapper.Map<AccomplishmentDto>(employee);
        }
    }
}
