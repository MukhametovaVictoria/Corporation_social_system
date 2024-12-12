namespace FrontEnd.Models.PersonalAccountModels
{
    public class CommunicationModel
    {
        public Guid Id { get; set; }
        public TypeCommunication Type { get; set; }
        public string Value { get; set; }
        public EmployeeModelFromPA? Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
