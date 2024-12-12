using BS.Contracts.Base;

namespace PersonalAccountV2.Models.Communication
{
    public class CreatingOrUpdatingCommunicationModel
    {
        public Guid Id { get; set; }
        public TypeCommunication Type { get; set; }
        public string Value { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
