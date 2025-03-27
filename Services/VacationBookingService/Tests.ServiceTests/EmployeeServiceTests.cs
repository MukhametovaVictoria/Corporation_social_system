using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests.ServiceTests
{
    public class EmployeeServiceTests
    {
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IEmployeeRepository> _mockRepo;

        public EmployeeServiceTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddEmployee_Should_Return_Created_Employee()
        {
#pragma warning disable S6562 // Always set the "DateTimeKind" when creating new "DateTime" instances
            var employee = new Employee { Id = Guid.NewGuid(), Name = "John", Surname = "Doe", DateOfBirth = new DateTime(1998, 4, 30) };
#pragma warning restore S6562 // Always set the "DateTimeKind" when creating new "DateTime" instances

            _mockRepo.Setup(repo => repo.AddEmployeeAsync(employee)).ReturnsAsync(employee);

            var result = await _employeeService.AddEmployeeAsync(employee);

            Assert.Equal(employee.Id, result.Id);
            Assert.Equal(employee.Name, result.Name);
        }

        // Другие тесты (Get, Update, Delete)...
    }
}