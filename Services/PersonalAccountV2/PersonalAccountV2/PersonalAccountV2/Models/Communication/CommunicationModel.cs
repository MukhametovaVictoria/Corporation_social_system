using BS.Contracts.Base;
using PersonalAccountV2.Models.Employee;

namespace PersonalAccountV2.Models.Communication
{
    public class CommunicationModel
    {
        public Guid Id { get; set; }
        public TypeCommunication Type { get; set; }
        public string Value { get; set; }
        public EmployeeModel? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
