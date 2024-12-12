using BS.Contracts.Base;
using BS.Contracts.Employee;

namespace BS.Contracts.Communication
{
    public class CommunicationDto
    {
        public Guid Id { get; set; }
        public TypeCommunication Type { get; set; }
        public string Value { get; set; }
        public EmployeeDto? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
