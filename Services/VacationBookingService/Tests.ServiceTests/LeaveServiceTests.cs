using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests.ServiceTests
{
    public class LeaveServiceTests
    {
        private readonly ILeaveService _leaveService;
        private readonly Mock<ILeaveRepository> _mockRepo;

        public LeaveServiceTests()
        {
            _mockRepo = new Mock<ILeaveRepository>();
            _leaveService = new LeaveService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddLeave_Should_Return_Created_Leave()
        {
            var leave = new Leave { Id = Guid.NewGuid(), EmployeeId = Guid.NewGuid(), StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(3) };

            _mockRepo.Setup(repo => repo.AddLeaveAsync(leave)).ReturnsAsync(leave);

            var result = await _leaveService.AddLeaveAsync(leave);

            Assert.Equal(leave.Id, result.Id);
        }

        // Другие тесты (Get, Update, Delete)...
    }
}
