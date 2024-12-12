using BS.Contracts.Accomplishment;

namespace BS.Services.Abstractions
{

    public interface IAccomplishmentService
    {
        Task<ICollection<AccomplishmentDto>> GetAllAccomplishmentEmployee(Guid employee);

        Task<AccomplishmentDto> GetByIdAsync(Guid id);

        public Task<Guid> CreateOrUpdate(CreatingOrUpdatingAccomplishmentDto accomplishmentEmployee);
    }
}
