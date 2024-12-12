using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts.Employee;
using Services.Repositories.Abstractions;

namespace Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получить список сотрудников.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Список сотрудников. </returns>
        public async Task<ICollection<ShortEmployeeDto>> GetPagedAsync(int page, int pageSize)
        {
            ICollection<Employee> entities = await _employeeRepository.GetPagedAsync(page, pageSize);
            var list = new List<ShortEmployeeDto>();
            foreach (var entity in entities)
            {
                list.Add(new ShortEmployeeDto()
                    {
                        Firstname = entity.FirstName,
                        Surname = entity.SurnName,
                        Position = entity.Position,
                        Id = entity.Id
                    }
                );
            }
            return list;
        }

        /// <summary>
        /// Получить новость.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО сотрудника. </returns>
        public async Task<ShortEmployeeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetAsync(id, cancellationToken);
            if (employee != null)
            {
                return new ShortEmployeeDto()
                {
                    Id = employee.Id,
                    Firstname = employee.FirstName,
                    Surname = employee.SurnName,
                    Position = employee.Position
                };
            }
            return null;
        }

        /// <summary>
        /// Создание/изменение сотрудников
        /// </summary>
        /// <param name="employees"> Список сотрудников. </param>
        public async Task<bool> CreateOrUpdateRange(List<ShortEmployeeDto> employees)
        {
            var list = employees.Where(x => x != null && x.Id != Guid.Empty).ToList();

            var newList = new List<Employee>();
            foreach (var employee in list)
            {
                newList.Add(new Employee() { FirstName = employee.Firstname, SurnName = employee.Surname, Position = employee.Position, Id = employee.Id });
            }
            var result = await _employeeRepository.CreateOrUpdateRange(newList);
            await _employeeRepository.SaveChangesAsync();

            return result;
        }
    }
}
